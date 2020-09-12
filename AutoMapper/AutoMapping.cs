using AutoMapper;
using RelibreApi.ViewModel;

namespace RelibreApi.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserViewModel, Models.User>()
            .ForPath(x => x.Person.Name, m => m.MapFrom(x => x.Name))
            .ForPath(x => x.Person.LastName, m => m.MapFrom(x => x.LastName))
            .ForPath(x => x.Person.Document, m => m.MapFrom(x => x.Document))
            .ReverseMap();

            CreateMap<PhoneViewModel, Models.Phone>().ReverseMap();

            CreateMap<AddressViewModel, Models.Address>().ReverseMap();

            CreateMap<AuthorViewModel, Models.AuthorBook>()
            .ForPath(x => x.Author.Name, m => m.MapFrom(x => x.Name))
            .ReverseMap();

            CreateMap<CategoryViewModel, Models.CategoryBook>()
            .ForPath(x => x.Category.Name, m => m.MapFrom(x => x.Name))
            .ReverseMap();
                        
            CreateMap<AuthorViewModel, Models.Author>().ReverseMap();

            CreateMap<CategoryViewModel, Models.Category>().ReverseMap();

            CreateMap<BookViewModel, Models.Book>()
            .ForPath(x => x.AuthorBooks, m => m.MapFrom(x => x.Authors))
            .ForPath(x => x.CategoryBooks, m => m.MapFrom(x => x.Categories))
            .ReverseMap();

            CreateMap<ContactViewModel, Models.Contact>().ReverseMap();

            CreateMap<ImageViewModel, Models.Image>().ReverseMap();

            CreateMap<TypeViewModel, Models.Type>().ReverseMap();

            CreateMap<LibraryViewModel, Models.Library>().ReverseMap();

            CreateMap<LibraryBookViewModel, Models.LibraryBook>().ReverseMap();
        }
    }
}