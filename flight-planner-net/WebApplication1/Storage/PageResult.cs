namespace WebApplication1.Storage
{
    public class PageResult <T>
    {
        public int Page;
        public int TotalItems;
        public List <T> Items;
    }
}
