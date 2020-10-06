using System.Collections.Generic;
using System.Linq;
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
            .ForPath(x => x.Person.Addresses, m => m.MapFrom(x => x.Addresses))
            .ReverseMap();

            CreateMap<UserRegisterViewModel, Models.User>()
            .ForPath(x => x.Person.Name, m => m.MapFrom(x => x.Name))
            .AfterMap((src, dest) =>
            {
                dest.Person.Phones = new List<Models.Phone>();

                dest.Person.Phones.Add(new Models.Phone
                {
                    Number = src.Phone
                });
            })
            .ReverseMap();

            CreateMap<PhoneViewModel, Models.Phone>().ReverseMap();

            CreateMap<AddressViewModel, Models.Address>().ReverseMap();

            CreateMap<AuthorViewModel, Models.AuthorBook>()
            .ForPath(x => x.Author.Name, m => m.MapFrom(x => x.Name))
            .ReverseMap();

            CreateMap<CategoryViewModel, Models.CategoryBook>()
            .ForPath(x => x.Category.Name, m => m.MapFrom(x => x.Name))
            .ReverseMap();

            CreateMap<AuthorViewModel, Models.Author>()
            .ForPath(x => x.Name, m => m.MapFrom(x => x.Name))
            .ReverseMap();

            CreateMap<CategoryViewModel, Models.Category>()
            .ForPath(x => x.Name, m => m.MapFrom(x => x.Name))
            .ReverseMap();

            CreateMap<BookViewModel, Models.Book>()
            .ForPath(x => x.AuthorBooks, m => m.MapFrom(x => x.Authors))
            .ForPath(x => x.CategoryBooks, m => m.MapFrom(x => x.Categories))
            .ReverseMap();

            CreateMap<ContactViewModel, Models.Contact>().ReverseMap();

            CreateMap<ImageViewModel, Models.Image>()
            .ForPath(x => x.Medium, m => m.MapFrom(x => x.Image))
            .ReverseMap();            
            
            CreateMap<TypeViewModel, Models.LibraryBookType>()
            .ForPath(x => x.Type.Id, m => m.MapFrom(x => x.id))
            .ForPath(x => x.Type.Description, m => m.MapFrom(x => x.Description))
            .ReverseMap();

            CreateMap<LibraryBookViewModel, Models.LibraryBook>()            
            .ForPath(x => x.LibraryBookTypes, m => m.MapFrom(x => x.Types))
            .ForPath(x => x.Library.Person.Addresses, m => m.MapFrom(x => x.Addresses))
            .ReverseMap();
        }
    }
}