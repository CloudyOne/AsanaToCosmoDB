namespace AsanaToCosmoDB.Models.Abstract
{
    public interface IPaginationResult
    {
        public PaginationPage next_page { get; set; }
    }
}

