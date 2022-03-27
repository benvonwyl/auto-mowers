namespace auto_mowers.Services.MowerService.Contract
{
    public class Mower
    {
        public Guid Id { get; set; }
        
        public Position P { get; set; }        
        
        public Guid LawnId { get;  set; }
    }
}
