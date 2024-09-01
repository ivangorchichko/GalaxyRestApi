namespace GalaxyRestApi.DAL.Models
{
    public class RegisteredEmployeeAuto : BaseModel
    {
        public int CarId { get;set; }
        public int EmployeeId { get;set; }

        public virtual required Car Car { get; set; }

        public virtual required Employee Employee { get; set; }
    }
}
