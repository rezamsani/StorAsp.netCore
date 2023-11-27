namespace Store.Application.Services.Orders.Command.AddNewOrder
{
    public class RequestAddNewOrderSericeDto
    {
        public long CartId { get; set; }
        public long RequestPayId { get; set; }
        public long UserId { get; set; }
        public string? Authority { get; set; }
        public long RefId { get; set; } = 0;

    }
}
