namespace BugTracker.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IssuesFormViewModel, Issue>();
            CreateMap<Issue, IssuesFormViewModel>();

        }

    }
}
