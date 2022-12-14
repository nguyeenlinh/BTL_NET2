namespace BTL_NET2.Models
{
  public class User
  {
    public string ID { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; }
    public string Password { get; set; }
  }
}