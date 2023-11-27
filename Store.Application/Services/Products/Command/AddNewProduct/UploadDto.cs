namespace Store.Application.Services.Products.Command.AddNewProduct
{
    public class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
