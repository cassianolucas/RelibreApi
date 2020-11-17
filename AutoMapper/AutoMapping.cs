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
            .ForPath(x => x.Person.BirthDate,m => m.MapFrom(x => x.BirthDate))
            .ForPath(x => x.Person.Document, m => m.MapFrom(x => x.Document))
            .ForPath(x => x.Person.Addresses, m => m.MapFrom(x => x.Addresses))
            .ForPath(x => x.Person.Phones, m => m.MapFrom(x => x.Phones))
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

            CreateMap<UserBusinessViewModel, Models.User>()
            .ForPath(x => x.Person.Id, m => m.MapFrom(x => x.Id))
            .ForPath(x => x.Person.Name, m => m.MapFrom(x => x.Name))
            .ForPath(x => x.Person.LastName, m => m.MapFrom(x => x.LastName))
            .ForPath(x => x.Person.WebSite, m => m.MapFrom(x => x.WebSite))
            .ForPath(x => x.Person.UrlImage, m => m.MapFrom(x => x.UrlImage))
            .ForPath(x => x.Person.Description, m => m.MapFrom(x => x.Description))
            .ForPath(x => x.Person.Document, m => m.MapFrom(x => x.Document))
            .ForPath(x => x.Person.Addresses, m => m.MapFrom(x => x.Addresses))            
            .AfterMap((src, dest) =>
            {
                if (dest.Person.PersonSubscriptions != null)
                {
                    src.ValidPlan = dest.Person.PersonSubscriptions
                        .SingleOrDefault().Validate;
                }

                dest.Person.Phones = new List<Models.Phone>();

                dest.Person.Phones.Add(new Models.Phone
                {
                    Number = src.Phone
                });                                
            });

            CreateMap<Models.User, UserBusinessViewModel>()
            .ForPath(x => x.Id, m => m.MapFrom(x => x.Person.Id))
            .ForPath(x => x.Name, m => m.MapFrom(x => x.Person.Name))
            .ForPath(x => x.LastName, m => m.MapFrom(x => x.Person.LastName))
            .ForPath(x => x.WebSite, m => m.MapFrom(x => x.Person.WebSite))
            .ForPath(x => x.UrlImage, m => m.MapFrom(x => x.Person.UrlImage))
            .ForPath(x => x.Description, m => m.MapFrom(x => x.Person.Description))
            .ForPath(x => x.Document, m => m.MapFrom(x => x.Person.Document))
            .ForPath(x => x.Addresses, m => m.MapFrom(x => x.Person.Addresses))
            .AfterMap((src, dest) =>
            {                
                if (src.Person.PersonSubscriptions != null)
                {
                    dest.ValidPlan = src.Person.PersonSubscriptions
                        .SingleOrDefault().Validate;
                }                

                if (src.Person.Phones != null)
                {
                    dest.Phone = src.Person.Phones
                        .SingleOrDefault(x => x.Master == true).Number;
                }                                                                                  
            });            

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

            CreateMap<ImageViewModel, Models.Image>()
            .ForPath(x => x.ImageLink, m => m.MapFrom(x => x.Image))
            .ReverseMap();

            CreateMap<TypeViewModel, Models.LibraryBookType>()
            .ForPath(x => x.Type.Id, m => m.MapFrom(x => x.Id))
            .ForPath(x => x.Type.Description, m => m.MapFrom(x => x.Description))
            .ReverseMap();

            CreateMap<LibraryBookViewModel, Models.LibraryBook>()
            .ForPath(x => x.LibraryBookTypes, m => m.MapFrom(x => x.Types))
            .ForPath(x => x.Library.Person.Addresses, m => m.MapFrom(x => x.Addresses))
            .ForPath(x => x.Library.Person.Name, m => m.MapFrom(x => x.Name))
            .ForPath(x => x.Description, m => m.MapFrom(x => x.Book.Description))
            .ReverseMap();

            CreateMap<ContactViewModel, Models.Contact>()
            .ReverseMap();
            
            CreateMap<ContactBookViewModel, Models.ContactBook>()
            .ForPath(x => x.Approved, m => m.MapFrom(x => x.Approved));

            CreateMap<Models.ContactBook, ContactBookViewModel>()
            .ForPath(x => x.Book, m => m.MapFrom(x => x.LibraryBook.Book))
            .AfterMap((src, dest) =>
            {
                if (src.ContactOwner == null)
                {                
                    dest.IdContact = src.IdContactRequest;
                    dest.FullName = src.ContactRequest.Name;
                    dest.CreatedAt = src.ContactRequest.CreatedAt;
                    dest.Email = src.ContactRequest.Email;
                }
                else
                {
                    dest.IdContact = src.IdContactOwner;
                    dest.FullName = src.ContactOwner.Name;
                    dest.CreatedAt = src.ContactOwner.CreatedAt;
                    dest.Email = src.ContactOwner.Email;
                }
                
                dest.Approved = src.Approved;
                dest.Denied = src.Denied;
                dest.IdLibraryBook = dest.IdLibraryBook;
            });

            CreateMap<Models.ContactBook, ContactBookApprovedViewModel>()
            .AfterMap((src, dest) =>
            {
                if (src.ContactOwner == null)
                {
                    dest.IdContact = src.IdContactRequest;
                    dest.Email = src.ContactRequest.Email;
                    dest.FullName = src.ContactRequest.Name;
                    dest.Phone = src.ContactRequest.Phone;
                    dest.CreatedAt = src.ContactRequest.CreatedAt;
                }
                else
                {
                    dest.IdContact = src.IdContactOwner;
                    dest.Email = src.ContactOwner.Email;
                    dest.FullName = src.ContactOwner.Name;
                    dest.Phone = src.ContactOwner.Phone;
                    dest.CreatedAt = src.ContactOwner.CreatedAt;
                }
            });

            CreateMap<NotificationPersonViewModel, Models.NotificationPerson>()
            .ForPath(x => x.Id, m => m.MapFrom(x => x.Id))
            .ForPath(x => x.Active, m => m.MapFrom(x => x.Active))
            .ForPath(x => x.CreatedAt, m => m.MapFrom(x => x.CreatedAt))
            .ForPath(x => x.Notification.Name, m => m.MapFrom(x => x.Name))
            .ForPath(x => x.Notification.Description, m => m.MapFrom(x => x.Description))
            .ReverseMap();

            CreateMap<NotificationPersonViewModel, Models.Notification>()
            .ForPath(x => x.Name, m => m.MapFrom(x => x.Name))
            .ForPath(x => x.Description, m => m.MapFrom(x => x.Description))
            .ReverseMap();

            CreateMap<SubscriptionViewModel, Models.PersonSubscription>()
            .ForPath(x => x.Subscription.Description, m => m.MapFrom(x => x.Description))
            .ForPath(x => x.Validate, m => m.MapFrom(x => x.Validate))
            .ForPath(x => x.PaidAt, m => m.MapFrom(x => x.PaidAt))
            .ForPath(x => x.Subscription.Value, m => m.MapFrom(x => x.Price))
            .ForPath(x => x.Subscription.Period, m => m.MapFrom(x => x.Period))
            .ReverseMap();

        }
    }
}