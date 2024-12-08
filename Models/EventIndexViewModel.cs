using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proiect_MPA_EB_Cantor_Andrei.Models
{
    public class EventIndexViewModel
    {
        public string SearchQuery { get; set; }
        public string SortOrder { get; set; }
        public List<SelectListItem> SortOrderList { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
