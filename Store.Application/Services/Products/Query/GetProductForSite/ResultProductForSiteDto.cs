namespace Store.Application.Services.Products.Query.GetProductForSite
{
    public class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }
}
