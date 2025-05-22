using AracKiralamaAPI.models;
using System.Data.SqlClient;
namespace AracKiralamaAPI.business.repository
{
    public interface IKiralamaRepository
    {
        IEnumerable<Kiralama> GetKiralamaKayitlari();
        Kiralama GetKiralamaKayitlari(int id);
        void InsertKiralamaKayit(Kiralama kayit);
    }
    public class KiralamaRepository:IKiralamaRepository
    {
        private readonly string _connectionString;

        public KiralamaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<Kiralama> GetKiralamaKayitlari()
        {
            var kayitlar = new List<Kiralama>();
            var con = new SqlConnection(_connectionString);
            con.Open();
            var cmd = new SqlCommand("SELECT * FROM kiralama", con);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                kayitlar.Add(new Kiralama
                {
                    id = (int)reader["id"],
                    k_ID = (int)reader["k_ID"],
                    a_ID = (int)reader["a_ID"],
                    baslangıc_tar = Convert.ToDateTime(reader["baslangıc_tar"]),
                    bitis_tar = Convert.ToDateTime(reader["bitis_tar"]),
                    kiralanma_adresi = reader["kiralanma_adresi"].ToString(),
                    toplam_ücret = (decimal)reader["toplam_ücret"],
                });
            }

            return kayitlar;
        }
        public Kiralama GetKiralamaKayitlari(int id)
        {
            Kiralama kiralama = null;
            var con = new SqlConnection(_connectionString);
            con.Open();
            var cmd = new SqlCommand("SELECT * FROM kiralama WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                kiralama = new Kiralama
                {
                    id = (int)reader["id"],
                    k_ID = (int)reader["k_ID"],
                    a_ID = (int)reader["a_ID"],
                    baslangıc_tar = Convert.ToDateTime(reader["baslangıc_tar"]),
                    bitis_tar = Convert.ToDateTime(reader["bitis_tar"]),
                    kiralanma_adresi = reader["kiralanma_adresi"].ToString(),
                    toplam_ücret = (decimal)reader["toplam_ücret"],
                };
            }

            return kiralama;
        }
        public void InsertKiralamaKayit(Kiralama kayit)
        {
            var con = new SqlConnection(_connectionString);
            con.Open();

            decimal gunlukUcret = 0;
            var cmdUcret = new SqlCommand("SELECT aGünlük_Ücret FROM arabalar WHERE aID = @aID", con);
            cmdUcret.Parameters.AddWithValue("@aID", kayit.a_ID);
            var result = cmdUcret.ExecuteScalar();
            if (result != null)
            {
                gunlukUcret = Convert.ToDecimal(result);
            }

            int gunSayisi = (kayit.bitis_tar - kayit.baslangıc_tar).Days;
            if (gunSayisi <= 0)
            {
                gunSayisi = 1; 
            }

            decimal toplamUcret = gunlukUcret * gunSayisi;

            var cmd = new SqlCommand(@"INSERT INTO kiralama (k_ID, a_ID, baslangıc_tar, bitis_tar, kiralanma_adresi, toplam_ücret)
                               VALUES (@k_ID, @a_ID, @baslangıc_tar, @bitis_tar, @kiralanma_adresi, @toplam_ücret)", con);

            cmd.Parameters.AddWithValue("@k_ID", kayit.k_ID);
            cmd.Parameters.AddWithValue("@a_ID", kayit.a_ID);
            cmd.Parameters.AddWithValue("@baslangıc_tar", kayit.baslangıc_tar);
            cmd.Parameters.AddWithValue("@bitis_tar", kayit.bitis_tar);
            cmd.Parameters.AddWithValue("@kiralanma_adresi", kayit.kiralanma_adresi);
            cmd.Parameters.AddWithValue("@toplam_ücret", toplamUcret);

            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}


