namespace ValidHealth.Data.Models
{
  public class AccessViewTreeModel
  {
    public long Id { get; set; }
    public string Object { get; set; }
    public long? ParentId { get; set; }
    public int Ins { get; set; }
    public int Upd { get; set; }
    public int Del { get; set; }
    public int Ron { get; set; }
  }
}
