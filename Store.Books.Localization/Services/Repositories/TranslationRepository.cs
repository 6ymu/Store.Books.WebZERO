namespace Store.Books.Localization.Services.Repositories
{
    using Infrastructure.Repos;
    using Model;
    using Services;
    using Interfaces;
    using Store.Books.Infrastructure;

    public class TranslationRepository : GenericRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(LocalizationDbContext context) : base(context) { }
    }
}
