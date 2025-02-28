using System;

namespace PizzaShop.Entity.ViewModel;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public int PageIndex { get; set; }

    public int TotalPages { get; set; }

    public PaginatedList(List<T> items, int PageIndex , int TotalPages)
    {
        this.Items = items;
        this.PageIndex = PageIndex;
        this.TotalPages = TotalPages;
    }

}
