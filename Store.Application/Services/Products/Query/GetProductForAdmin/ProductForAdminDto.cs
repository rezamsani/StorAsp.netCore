﻿namespace Store.Application.Services.Products.Query.GetProductForAdmin
{
    public class ProductForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<ProductsFormAdminList_Dto> Products { get; set; }
    }
}
