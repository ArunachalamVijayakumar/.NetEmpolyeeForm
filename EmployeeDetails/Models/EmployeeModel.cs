using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string designation { get; set; }

        public ProjectDetail Project { get; set; }

    }

    public class ProjectDetail
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }

        public string ProjectManagerName { get; set; }
    }

}
