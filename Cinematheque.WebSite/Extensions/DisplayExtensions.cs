using Cinematheque.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Cinematheque.WebSite.Extensions
{
    public static class DisplayExtensions
    {
        public static List<SelectListItem> ConvertToListItems(this IEnumerable<string> @items)
        {
            return items.Select(i => new SelectListItem() { Text = i, Value = i }).ToList();
        }

        public static List<SelectListItem> ConvertToListItems(this IEnumerable<Director> @items)
        {
            return items.Select(i => new SelectListItem() { Text = i.GetFullName(), Value = i.ID.ToString() }).ToList();
        }

        public static List<SelectListItem> ConvertToListItems(this IEnumerable<Actor> @items)
        {
            return items.Select(i => new SelectListItem() { Text = i.GetFullName(), Value = i.ID.ToString() }).ToList();
        }

        public static List<SelectListItem> ConvertToListItems(this IEnumerable<Genre> @items)
        {
            return items.Select(i => new SelectListItem() { Text = i.Name, Value = i.ID.ToString() }).ToList();
        }

        public static List<SelectListItem> ConvertToCheckBoxItems(this IEnumerable<string> @items, IEnumerable<string> checkedItems)
        {
            return items.Select(i => checkedItems.Contains(i) ? 
                                      new SelectListItem() { Text = i, Value = i, Selected = true } : 
                                      new SelectListItem() { Text = i, Value = i, Selected = false })
                        .ToList();

        }
    }
}