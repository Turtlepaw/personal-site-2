namespace Pepperbot.Data
{
    public record RedirectLink
    {
        public string URL { get; set; }
        public DateTime RedirectTS { get; set; }
        public string Name { get; set; }
    }
}
