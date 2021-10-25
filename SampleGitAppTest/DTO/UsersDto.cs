namespace SampleAppTest
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Login { get; set; }

        public string Company { get; set; }

        public int NoOfFollowers { get; set; }

        public int NoOfPublicRepsitories { get; set; }

        public float AvgFollowersPerRepo { get  { return NoOfPublicRepsitories == 0 
                    ? NoOfPublicRepsitories 
                    : (float)NoOfFollowers / NoOfPublicRepsitories; } }

      
    }
}

