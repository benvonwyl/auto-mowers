namespace auto_mowers.Services.MowerService.Request
{
    public class AddMowerRequest
    {
        public PositionRequest? Position { get; set; }        
        
        public Guid LawnId { get; set; }
    }
}
