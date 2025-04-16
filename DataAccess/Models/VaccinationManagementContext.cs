using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Vaccination.DataAccess.Models
{
    public class VaccinationManagementContext : DbContext
    {
        public VaccinationManagementContext()
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InjectionResult> InjectionResults { get; set; }
        public DbSet<InjectionSchedule> InjectionSchedules { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineType> VaccineTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1, // Id từ BaseEntity
                    FullName = "John Doe",
                    Email = "johndoe@example.com",
                    Username = "johndoe",
                    Password = "password123",
                    Phone = "123456789",
                    Address = "123 Main Street",
                    DateOfBirth = new DateTime(1985, 1, 1),
                    Gender = true,
                    IdentityCard = "123456789012"
                },
                new Customer
                {
                    Id = 2,
                    FullName = "Jane Smith",
                    Email = "janesmith@example.com",
                    Username = "janesmith",
                    Password = "password123",
                    Phone = "987654321",
                    Address = "456 Second Avenue",
                    DateOfBirth = new DateTime(1990, 2, 15),
                    Gender = false,
                    IdentityCard = "987654321098"
                }
            );

            // Seed dữ liệu cho Employee
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1, // Id từ BaseEntity
                    EmployeeName = "Michael Scott",
                    Email = "michael.scott@dundermifflin.com",
                    Username = "michaelscott",
                    Password = "password123",
                    Phone = "555-1234",
                    Address = "Scranton, PA",
                    DateOfBirth = new DateTime(1964, 3, 15),
                    Gender = true,
                    Position = "Regional Manager",
                    Image = "/images/michael.jpg",
                    WorkingPlace = "Dunder Mifflin Scranton"
                },
                new Employee
                {
                    Id = 2,
                    EmployeeName = "Pam Beesly",
                    Email = "pam.beesly@dundermifflin.com",
                    Username = "pambeesly",
                    Password = "password123",
                    Phone = "555-5678",
                    Address = "Scranton, PA",
                    DateOfBirth = new DateTime(1979, 3, 25),
                    Gender = false,
                    Position = "Receptionist",
                    Image = "/images/pam.jpg",
                    WorkingPlace = "Dunder Mifflin Scranton"
                },
                new Employee
                {
                    Id = 3,
                    EmployeeName = "Jim Halpert",
                    Email = "jim.halpert@dundermifflin.com",
                    Username = "jimhalpert",
                    Password = "password123",
                    Phone = "555-7890",
                    Address = "Scranton, PA",
                    DateOfBirth = new DateTime(1978, 10, 1),
                    Gender = true,
                    Position = "Salesman",
                    Image = "/images/jim.jpg",
                    WorkingPlace = "Dunder Mifflin Scranton"
                }
            );

            // Seed dữ liệu cho VaccineType
            modelBuilder.Entity<VaccineType>().HasData(
                new VaccineType
                {
                    Id = 1, // Id từ BaseEntity
                    VaccineTypeCode = "VT00001",
                    VaccineTypeName = "COVID-19",
                    Description = "Vaccine cho virus SARS-CoV-2"
                },
                new VaccineType
                {
                    Id = 2,
                    VaccineTypeCode = "VT00002",
                    VaccineTypeName = "Influenza",
                    Description = "Vaccine chống cúm mùa"
                },
                new VaccineType
                {
                    Id = 3,
                    VaccineTypeCode = "VT00003",
                    VaccineTypeName = "Hepatitis B",
                    Description = "Vaccine chống viêm gan B"
                },
                new VaccineType
                {
                    Id = 4,
                    VaccineTypeCode = "VT00004",
                    VaccineTypeName = "Tetanus",
                    Description = "Vaccine chống uốn ván"
                },
                new VaccineType
                {
                    Id = 5,
                    VaccineTypeCode = "VT00005",
                    VaccineTypeName = "Measles",
                    Description = "Vaccine chống bệnh sởi"
                }
            );

            // Seed dữ liệu cho Vaccine
            modelBuilder.Entity<Vaccine>().HasData(
                new Vaccine
                {
                    Id = 1, // Id từ BaseEntity
                    VaccineName = "Pfizer-BioNTech",
                    Contraindication = "Không dành cho trẻ dưới 12 tuổi",
                    Indication = "Người từ 12 tuổi trở lên",
                    NumberOfInjection = 2,
                    Origin = "USA",
                    NextBeginNextInjection = new DateTime(2023, 12, 1),
                    NextEndNextInjection = new DateTime(2023, 12, 15),
                    Usage = "Tiêm bắp",
                    VaccineTypeId = 1 // Liên kết với loại vaccine COVID-19
                },
                new Vaccine
                {
                    Id = 2,
                    VaccineName = "Moderna",
                    Contraindication = "Không dành cho người có tiền sử dị ứng",
                    Indication = "Người từ 18 tuổi trở lên",
                    NumberOfInjection = 2,
                    Origin = "USA",
                    NextBeginNextInjection = new DateTime(2023, 11, 1),
                    NextEndNextInjection = new DateTime(2023, 11, 14),
                    Usage = "Tiêm bắp",
                    VaccineTypeId = 1 // Liên kết với loại vaccine COVID-19
                },
                new Vaccine
                {
                    Id = 3,
                    VaccineName = "Vaxigrip Tetra",
                    Contraindication = "Không dành cho người bị sốt cao",
                    Indication = "Người lớn và trẻ em từ 6 tháng tuổi",
                    NumberOfInjection = 1,
                    Origin = "France",
                    NextBeginNextInjection = new DateTime(2023, 10, 1),
                    NextEndNextInjection = new DateTime(2023, 10, 31),
                    Usage = "Tiêm bắp",
                    VaccineTypeId = 2 // Liên kết với loại vaccine Influenza (cúm mùa)
                },
                new Vaccine
                {
                    Id = 4,
                    VaccineName = "Engerix-B",
                    Contraindication = "Không dành cho người có tiền sử dị ứng",
                    Indication = "Người chưa tiêm phòng viêm gan B",
                    NumberOfInjection = 3,
                    Origin = "UK",
                    NextBeginNextInjection = new DateTime(2023, 9, 1),
                    NextEndNextInjection = new DateTime(2023, 9, 15),
                    Usage = "Tiêm bắp",
                    VaccineTypeId = 3 // Liên kết với loại vaccine Hepatitis B (viêm gan B)
                },
                new Vaccine
                {
                    Id = 5,
                    VaccineName = "Tetavax",
                    Contraindication = "Không dành cho người bị dị ứng",
                    Indication = "Người từ 6 tuổi trở lên",
                    NumberOfInjection = 1,
                    Origin = "France",
                    NextBeginNextInjection = new DateTime(2024, 1, 1),
                    NextEndNextInjection = new DateTime(2024, 1, 31),
                    Usage = "Tiêm bắp",
                    VaccineTypeId = 4 // Liên kết với loại vaccine Tetanus (uốn ván)
                }
            );

            // Seed dữ liệu cho InjectionResult
            modelBuilder.Entity<InjectionResult>().HasData(
                new InjectionResult
                {
                    Id = 1, // Id từ BaseEntity
                    InjectionDate = new DateTime(2023, 7, 15),
                    InjectionPlace = "Bệnh viện Nhi Đồng",
                    NextInjectionDate = new DateTime(2024, 7, 15),
                    NumberOfInjection = 1,
                    Prevention = "COVID-19",
                    CustomerId = 1, // Liên kết với Customer có Id là 1
                    VaccineId = 1 // Liên kết với Vaccine có Id là 1 (Pfizer-BioNTech)
                },
                new InjectionResult
                {
                    Id = 2,
                    InjectionDate = new DateTime(2023, 8, 20),
                    InjectionPlace = "Phòng khám Đa khoa ABC",
                    NextInjectionDate = new DateTime(2024, 8, 20),
                    NumberOfInjection = 2,
                    Prevention = "Influenza",
                    CustomerId = 2, // Liên kết với Customer có Id là 2
                    VaccineId = 3 // Liên kết với Vaccine có Id là 3 (Vaxigrip Tetra)
                },
                new InjectionResult
                {
                    Id = 3,
                    InjectionDate = new DateTime(2023, 6, 10),
                    InjectionPlace = "Trung tâm Y tế quận 7",
                    NextInjectionDate = new DateTime(2024, 6, 10),
                    NumberOfInjection = 1,
                    Prevention = "Tetanus",
                    CustomerId = 2, // Liên kết với Customer có Id là 3
                    VaccineId = 5 // Liên kết với Vaccine có Id là 5 (Tetavax)
                }
            );

            // Seed dữ liệu cho InjectionSchedule
            modelBuilder.Entity<InjectionSchedule>().HasData(
                new InjectionSchedule
                {
                    Id = 1, // Id từ BaseEntity
                    Description = "Lịch tiêm vaccine phòng COVID-19 đợt 1",
                    StartDate = new DateTime(2024, 1, 10),
                    EndDate = new DateTime(2024, 1, 20),
                    Place = "Trung tâm y tế quận 1",
                    VaccineId = 1, // Liên kết với Vaccine có Id là 1 (Pfizer-BioNTech)
                    Status = ScheduleStatus.Open,
                    DateCreated = DateTime.Now
                },
                new InjectionSchedule
                {
                    Id = 2,
                    Description = "Lịch tiêm phòng cúm mùa đợt 2",
                    StartDate = new DateTime(2024, 3, 15),
                    EndDate = new DateTime(2024, 3, 25),
                    Place = "Bệnh viện Đa khoa quốc tế",
                    VaccineId = 3, // Liên kết với Vaccine có Id là 3 (Vaxigrip Tetra)
                    Status = ScheduleStatus.Over,
                    DateCreated = DateTime.Now
                },
                new InjectionSchedule
                {
                    Id = 3,
                    Description = "Lịch tiêm phòng uốn ván đợt 1",
                    StartDate = new DateTime(2024, 5, 1),
                    EndDate = new DateTime(2024, 5, 10),
                    Place = "Trung tâm y tế quận 7",
                    VaccineId = 5, // Liên kết với Vaccine có Id là 5 (Tetavax)
                    DateCreated = DateTime.Now
                }
            );

            // Seed dữ liệu cho NewsType
            modelBuilder.Entity<NewsType>().HasData(
                new NewsType
                {
                    Id = 1, // Id từ BaseEntity
                    NewsTypeName = "Health",
                    Description = "Tin tức về sức khỏe, y tế và các bệnh truyền nhiễm."
                },
                new NewsType
                {
                    Id = 2,
                    NewsTypeName = "Technology",
                    Description = "Tin tức về công nghệ, phần mềm và các xu hướng mới."
                },
                new NewsType
                {
                    Id = 3,
                    NewsTypeName = "Education",
                    Description = "Tin tức về giáo dục, đào tạo và học đường."
                }
            );

            // Seed dữ liệu cho News
            modelBuilder.Entity<News>().HasData(
             new News
             {
                 Id = 1,
                 Title = "Đột phá trong nghiên cứu chữa trị ung thư",
                 Preview = "Nghiên cứu mới trong điều trị ung thư mang lại hy vọng cho nhiều bệnh nhân...",
                 Content = "Các nhà khoa học đã phát hiện ra một loại thuốc có thể tiêu diệt tế bào ung thư mà không làm tổn thương tế bào lành...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 1, 15)
             },
             new News
             {
                 Id = 2,
                 Title = "Trí tuệ nhân tạo và tương lai công nghệ",
                 Preview = "AI sẽ thay đổi cách con người làm việc và tương tác với công nghệ trong tương lai gần...",
                 Content = "AI đã tạo ra những đột phá lớn trong các ngành công nghiệp từ y tế đến giáo dục...",
                 NewsTypeId = 2,
                 PostDate = new DateTime(2023, 2, 10)
             },
             new News
             {
                 Id = 3,
                 Title = "Cải cách giáo dục trong thời đại số",
                 Preview = "Những thay đổi trong giáo dục nhằm đáp ứng nhu cầu của thế hệ học sinh số hóa...",
                 Content = "Công nghệ đang thay đổi cách chúng ta dạy và học, với các công cụ như AI và học trực tuyến...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 3, 25)
             },
             new News
             {
                 Id = 4,
                 Title = "Công nghệ 5G thúc đẩy chuyển đổi số",
                 Preview = "5G mở ra những tiềm năng cho các ngành công nghiệp và kết nối xã hội...",
                 Content = "Công nghệ 5G đã đưa internet vào tốc độ cao hơn, thúc đẩy phát triển IoT và smart cities...",
                 NewsTypeId = 2,
                 PostDate = new DateTime(2023, 4, 5)
             },
             new News
             {
                 Id = 5,
                 Title = "Vaccine mới cho bệnh cúm mùa",
                 Preview = "Vaccine cải tiến giúp phòng chống cúm mùa hiệu quả hơn...",
                 Content = "Các nhà nghiên cứu đã tạo ra một loại vaccine mới bảo vệ người dân khỏi các biến thể cúm mùa...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 4, 20)
             },
             new News
             {
                 Id = 6,
                 Title = "Ứng dụng blockchain trong giáo dục",
                 Preview = "Blockchain mang lại tính minh bạch và bảo mật cao cho dữ liệu học sinh...",
                 Content = "Công nghệ blockchain đang được xem xét ứng dụng để bảo mật dữ liệu học sinh và văn bằng giáo dục...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 5, 12)
             },
             new News
             {
                 Id = 7,
                 Title = "Phòng chống dịch bệnh với công nghệ AI",
                 Preview = "AI đang giúp phát hiện và phòng chống dịch bệnh một cách hiệu quả...",
                 Content = "Các ứng dụng AI đang giúp giám sát, dự đoán và ngăn chặn sự lây lan của dịch bệnh...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 5, 28)
             },
             new News
             {
                 Id = 8,
                 Title = "Thay đổi trong kỳ thi THPT quốc gia",
                 Preview = "Bộ giáo dục điều chỉnh một số quy định cho kỳ thi THPT quốc gia...",
                 Content = "Những thay đổi mới trong kỳ thi THPT sẽ tạo điều kiện thuận lợi hơn cho thí sinh...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 6, 1)
             },
             new News
             {
                 Id = 9,
                 Title = "Ứng dụng công nghệ trong điều trị tâm lý",
                 Preview = "Sử dụng công nghệ VR và AI trong các phương pháp trị liệu tâm lý...",
                 Content = "Các công nghệ mới giúp bệnh nhân tâm lý thoát khỏi căng thẳng và lo âu...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 6, 15)
             },
             new News
             {
                 Id = 10,
                 Title = "Robot học tập hỗ trợ trong giảng dạy",
                 Preview = "Robot hỗ trợ giáo viên giảng dạy và tương tác với học sinh...",
                 Content = "Các robot AI giúp tạo hứng thú trong lớp học và cải thiện kết quả học tập...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 6, 30)
             },
             new News
             {
                 Id = 11,
                 Title = "Phát triển vaccine chống COVID-19 thế hệ mới",
                 Preview = "Nghiên cứu vaccine thế hệ mới chống các biến thể COVID-19...",
                 Content = "Các loại vaccine mới có thể đối phó tốt hơn với biến thể mới của COVID-19...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 7, 10)
             },
             new News
             {
                 Id = 12,
                 Title = "Dạy lập trình từ xa hiệu quả hơn nhờ AI",
                 Preview = "AI giúp tối ưu hoá việc học lập trình từ xa và nâng cao hiệu quả...",
                 Content = "AI hỗ trợ tự động đánh giá và sửa lỗi, giúp học viên nhanh chóng cải thiện kỹ năng...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 7, 21)
             },
             new News
             {
                 Id = 13,
                 Title = "Hệ thống bảo mật mới cho bệnh viện",
                 Preview = "Hệ thống bảo mật mới nhằm bảo vệ dữ liệu bệnh nhân trong bệnh viện...",
                 Content = "Các bệnh viện đang triển khai hệ thống bảo mật hiện đại để đảm bảo an toàn dữ liệu...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 7, 25)
             },
             new News
             {
                 Id = 14,
                 Title = "Điện toán đám mây trong y tế",
                 Preview = "Lưu trữ dữ liệu y tế trên cloud giúp giảm chi phí và tăng tốc độ xử lý...",
                 Content = "Nhiều bệnh viện đang sử dụng cloud để lưu trữ dữ liệu y tế một cách an toàn...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 8, 1)
             },
             new News
             {
                 Id = 15,
                 Title = "Giáo dục trực tuyến phát triển mạnh",
                 Preview = "Đại dịch đã thúc đẩy sự phát triển của giáo dục trực tuyến...",
                 Content = "Các nền tảng học trực tuyến đang thu hút hàng triệu học sinh tham gia...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 8, 15)
             },
             new News
             {
                 Id = 16,
                 Title = "Điện thoại thông minh mới nhất của năm",
                 Preview = "Điện thoại mới của hãng công nghệ nổi tiếng với các tính năng vượt trội...",
                 Content = "Điện thoại mới có camera AI, hiệu năng cao, và thời lượng pin lâu hơn...",
                 NewsTypeId = 2,
                 PostDate = new DateTime(2023, 8, 25)
             },
             new News
             {
                 Id = 17,
                 Title = "Tác động của internet tốc độ cao tới xã hội",
                 Preview = "Internet tốc độ cao thay đổi cách mọi người kết nối và làm việc...",
                 Content = "Internet tốc độ cao thúc đẩy nền kinh tế số và các mô hình làm việc mới...",
                 NewsTypeId = 2,
                 PostDate = new DateTime(2023, 9, 5)
             },
             new News
             {
                 Id = 18,
                 Title = "Điều trị bệnh tim mạch bằng công nghệ mới",
                 Preview = "Công nghệ mới giúp điều trị bệnh tim hiệu quả và an toàn hơn...",
                 Content = "Các công nghệ tiên tiến giúp phát hiện và điều trị sớm bệnh tim mạch...",
                 NewsTypeId = 1,
                 PostDate = new DateTime(2023, 9, 15)
             },
             new News
             {
                 Id = 19,
                 Title = "Khoa học dữ liệu và giáo dục hiện đại",
                 Preview = "Khoa học dữ liệu giúp tối ưu hóa phương pháp giảng dạy và học tập...",
                 Content = "Việc ứng dụng khoa học dữ liệu vào giáo dục giúp cá nhân hóa lộ trình học tập...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 9, 20)
             },
             new News
             {
                 Id = 20,
                 Title = "Hệ thống giám sát môi trường bằng AI",
                 Preview = "AI hỗ trợ giám sát và cảnh báo tình trạng môi trường tại nhiều quốc gia...",
                 Content = "Hệ thống AI thu thập và phân tích dữ liệu môi trường nhằm cảnh báo sớm...",
                 NewsTypeId = 2,
                 PostDate = new DateTime(2023, 9, 25)
             },
             new News
             {
                 Id = 21,
                 Title = "Ứng dụng học ngôn ngữ AI cho trẻ em",
                 Preview = "Ứng dụng AI giúp trẻ em học ngoại ngữ một cách tự nhiên và hiệu quả...",
                 Content = "Ứng dụng giúp trẻ em tiếp xúc với ngoại ngữ từ sớm, tạo nền tảng học tập sau này...",
                 NewsTypeId = 3,
                 PostDate = new DateTime(2023, 10, 1)
             }
            );
        }
    }
}
