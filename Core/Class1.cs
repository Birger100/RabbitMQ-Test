namespace Core
{
    public class QueueObject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ListItemObj>? Items  { get; set; }
    }

    public class ListItemObj
    {
        public string? Name { get; set; }
    }

}