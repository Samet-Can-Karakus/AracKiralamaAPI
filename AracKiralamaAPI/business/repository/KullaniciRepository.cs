using AracKiralamaAPI.models;
using System.Data.SqlClient;


namespace AracKiralamaAPI.business.repository
{

    public interface IKullaniciRepository
    {
        IEnumerable<Kullanici> GetKullaniciKayitlari();
        Kullanici GetKullaniciKayitlari(int id);
        void InsertKullaniciKayit(Kullanici kayit);
    }
        public class KullaniciRepository:IKullaniciRepository    
    {
        private readonly string _connectionString;

        public KullaniciRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Kullanici> GetKullaniciKayitlari()
        {
            var kayitlar = new List<Kullanici>();
            var con = new SqlConnection(_connectionString);
            con.Open();
            var cmd = new SqlCommand("SELECT * FROM kullaniciler", con);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
                kayitlar.Add(new Kullanici
                {
                    kID = (int)reader["kID"],
                    kMail = reader["kMail"].ToString(),
                    kSifre = reader["kSifre"].ToString(),
                });
            return kayitlar;
        }

        public Kullanici GetKullaniciKayitlari(int id)
        {
            var kayitlar = new List<Kullanici>();
            var con = new SqlConnection(_connectionString);
            con.Open();
            var cmd = new SqlCommand("SELECT * FROM kullaniciler Where kID=@kID", con);
            cmd.Parameters.AddWithValue("@kID", id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                kayitlar.Add(new Kullanici
                {
                    kID = (int)reader["kID"],
                    kMail = reader["kMail"].ToString(),
                    kSifre = reader["kSifre"].ToString(),
                });

            con.Close();

            return kayitlar[0];

        }

        public void InsertKullaniciKayit(Kullanici kayit)
        {
            var con = new SqlConnection(_connectionString);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO kullaniciler (kMail, kSifre) VALUES (@kMail, @kSifre)", con);
            cmd.Parameters.AddWithValue("@kMail", kayit.kMail);
            cmd.Parameters.AddWithValue("@kSifre", kayit.kSifre);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
