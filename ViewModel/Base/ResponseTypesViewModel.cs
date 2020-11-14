using System.Collections.Generic;

namespace RelibreApi.ViewModel
{
    public class ResponseTypesViewModel
    {
        public IEnumerable<LibraryBookViewModel> Matches { get; set; }
        public IEnumerable<LibraryBookViewModel> Books { get; set; }
    }
}