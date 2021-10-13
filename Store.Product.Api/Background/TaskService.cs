using ExcelDataReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Books.Infrastructure.Interfaces;
using Store.Common.Configs;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Books.Localization.Background
{
    public class TaskService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;
        private readonly ServiceConfig _config;

        public TaskService(IServiceProvider provider,
            IOptions<ServiceConfig> config,
            ILogger<TaskService> logger)
        {
            _provider = provider;
            _logger = logger;
            _config = config?.Value;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _provider.CreateScope();
            var authors = scope.ServiceProvider.GetRequiredService<IAuthorRepository>();//
            var books = scope.ServiceProvider.GetRequiredService<IBookRepository>();    //
            var genres = scope.ServiceProvider.GetRequiredService<IGenreRepository>();  //
            var prices = scope.ServiceProvider.GetRequiredService<IPriceRepository>();  //
            var bookgenres = scope.ServiceProvider.GetRequiredService<IBookGenreRepository>(); //
            var bookauthors = scope.ServiceProvider.GetRequiredService<IBookAuthorRepository>();

            while (!stoppingToken.IsCancellationRequested)
            {
                using var stream = File.Open(_config.ExcelFile, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var counter = 0;
                do
                {
                    while (reader.Read())
                    {
                        if (counter == 0) { counter++; continue; }
                        var newTitle = reader.GetString(0);
                        var newAuthorFirstName = reader.GetString(1);
                        var newAuthorSecondName = reader.GetString(2);
                        var newGenre = reader.GetString(3);
                        var newYear = Convert.ToInt32(reader.GetDouble(4));
                        var newPrice = Convert.ToDecimal(reader.GetDouble(5));

                        var dbBookCheck = (await books.Get(p => p.Title == newTitle)).FirstOrDefault();
                        if (dbBookCheck is null)
                        {
                            dbBookCheck = new Domain.Book { Title = newTitle, Year = newYear };
                            await books.Insert(dbBookCheck);
                        }
                            
                        var dbAuthorCheck = (await authors.Get(p => p.FirstName == newAuthorFirstName&& p.SurName== newAuthorSecondName)).FirstOrDefault();
                        if (dbAuthorCheck is null)
                        {
                            dbAuthorCheck = new Domain.Author { FirstName = newAuthorFirstName, SurName = newAuthorSecondName };
                            await authors.Insert(dbAuthorCheck);
                        }
                           
                        var dbGenreCheck = (await genres.Get(p => p.Title == newTitle)).FirstOrDefault();
                        if (dbGenreCheck is null)
                        {
                            dbGenreCheck = new Domain.Genre { Title = newTitle };
                            await genres.Insert(dbGenreCheck);
                        }
                            
                        DateTime currentDate = DateTime.Now;
                        var dbPrices = (await prices.Get(p => p.Book == dbBookCheck && p.Created == currentDate)).FirstOrDefault();
                        if (!(dbPrices is null))
                            await prices.Delete(dbPrices);
                        await prices.Insert(new Domain.Price { Book = dbBookCheck, Created = currentDate, Amount = newPrice });
                        
                        var dbBookGenreCheck = (await bookgenres.Get(p => p.Book == dbBookCheck && p.Genre == dbGenreCheck)).FirstOrDefault();
                        if (dbBookGenreCheck is null)
                        {
                            dbBookGenreCheck = new Domain.BookGenre { Book = dbBookCheck, Genre = dbGenreCheck};
                            await bookgenres.Insert(dbBookGenreCheck);
                        }

                        var dbBookAuthorCheck = (await bookauthors.Get(p => p.Book == dbBookCheck && p.Author== dbAuthorCheck)).FirstOrDefault();
                        if (dbBookAuthorCheck is null)
                        {
                            dbBookAuthorCheck = new Domain.BookAuthor { Book = dbBookCheck, Author = dbAuthorCheck };
                            await bookauthors.Insert(dbBookAuthorCheck);
                        }

                    }
                } while (reader.NextResult());
                Thread.Sleep(10 * 1000);
            }
            //return Task.CompletedTask;
        }
    }
}
