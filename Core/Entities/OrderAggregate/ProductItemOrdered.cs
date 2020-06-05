namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemid, string productName, string pictureUrl)
        {
        ProductItemid = productItemid;
        ProductName = productName;
        PictureUrl = pictureUrl;
        }

        public int ProductItemid { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}