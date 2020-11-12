using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RelibreApi.ViewModel
{
    public class BookViewModel : BaseCreatedViewModel
    {                
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "code_integration")]
        [Required(ErrorMessage = "Necessário informar o código de integração!")]
        [MinLength(4, ErrorMessage = "Código inválido!")]
        public string CodeIntegration { get; set; }

        [JsonProperty(PropertyName = "isbn_13")]
        [Required(ErrorMessage = "Necessário informar o código ISBN13!")]
        [MinLength(4, ErrorMessage = "Código ISBN13 inválido!")]
        public string Isbn13 { get; set; }

        [JsonProperty(PropertyName = "title")]
        [Required(ErrorMessage = "Necessário informar o título!")]
        [MinLength(4, ErrorMessage = "Título inválido!")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "maturity_rating")]
        public string MaturityRating { get; set; }

        [JsonProperty(PropertyName = "average_rating")]
        public string AverageRating { get; set; }

        [JsonProperty(PropertyName = "authors")]
        public List<AuthorViewModel> Authors { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public List<CategoryViewModel> Categories { get; set; }
    }
}