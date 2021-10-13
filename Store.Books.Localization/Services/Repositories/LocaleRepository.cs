namespace Store.Books.Localization.Services.Repositories
{
    using Infrastructure.Repos;
    using Model;
    using Services;
    using Interfaces;
    using Store.Books.Infrastructure;

    public class LocaleRepository : GenericRepository<Locale>, ILocaleRepository
    {
        public LocaleRepository(LocalizationDbContext context) : base(context) { }
    }
}
