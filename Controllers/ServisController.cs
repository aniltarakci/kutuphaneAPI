using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kutuphane.Models;
using kutuphane.ViewModel;

namespace kutuphane.Controllers
{
    public class ServisController : ApiController
    {
        KUTUPHANEEntities db = new KUTUPHANEEntities();
        SonucModel sonuc = new SonucModel();

        #region Calisanlar

        [HttpGet]
        [Route("api/calisanlarliste")]

        public List<CalisanlarModel> CalisanlarListe()
        {
            List<CalisanlarModel> liste = db.Calisanlar.Select(x => new CalisanlarModel()
            {
                Calisan_id = x.Calisan_id,
                Ad = x.Ad,
                Soyad = x.Soyad,
                Tel_no = x.Tel_no,
                Adres = x.Adres

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/calisanlarbyid/{calisanid}")]

        public CalisanlarModel Calisanlarbyid(int calisanid)
        {
            CalisanlarModel kayit = db.Calisanlar.Where(s => s.Calisan_id == calisanid).Select(x =>
            new CalisanlarModel()
            {
                Calisan_id = x.Calisan_id,
                Ad = x.Ad,
                Soyad = x.Soyad,
                Tel_no = x.Tel_no,
                Adres = x.Adres
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/calisanlarekle")]

        public SonucModel CalisanEkle(CalisanlarModel model)
        {
            Calisanlar yeni = new Calisanlar();
            yeni.Ad = model.Ad;
            yeni.Soyad = model.Soyad;
            yeni.Tel_no = model.Tel_no;
            yeni.Adres = model.Adres;
            db.Calisanlar.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Çalışan eklendi";
            return sonuc;
        }


        [HttpPut]
        [Route("api/calisanlarduzenle")]

        public SonucModel CalisanDuzenle(CalisanlarModel model)
        {
            Calisanlar kayit = db.Calisanlar.Where(s => s.Calisan_id == model.Calisan_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Ad = model.Ad;
            kayit.Soyad = model.Soyad;
            kayit.Tel_no = model.Tel_no;
            kayit.Adres = model.Adres;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Çalışanlar düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/calisanlarsil/{calisanid}")]

        public SonucModel CalisanSil(int calisanid)
        {
            Calisanlar kayit = db.Calisanlar.Where(s => s.Calisan_id == calisanid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            if (db.KitapKiralama.Count(s => s.Calisan_id == calisanid) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Kitap Kiralama kayıtlı kategori silinemez!";
                return sonuc;
            }

            db.Calisanlar.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Çalışan silindi.";

            return sonuc;
        }


        [HttpGet]
        [Route("api/calisansayisi")]

        public int CalisanSayisi()
        {
            return db.Calisanlar.Count();
        }


     

        #endregion

        #region Firmalar

        [HttpGet]
        [Route("api/firmalarliste")]

        public List<FirmalarModel> FirmalarListe()
        {
            List<FirmalarModel> liste = db.Firmalar.Select(x => new FirmalarModel()
            {
                Firma_id = x.Firma_id,
                Firma_adi = x.Firma_adi,
                Tel = x.Tel,
                Fax = x.Fax,
                Eposta_adresi = x.Eposta_adresi
            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/firmalarbyid/{firmaid}")]

        public FirmalarModel Firmalarbyid(int firmaid)
        {
            FirmalarModel kayit = db.Firmalar.Where(s => s.Firma_id == firmaid).Select(x =>
            new FirmalarModel()
            {
                Firma_id = x.Firma_id,
                Firma_adi = x.Firma_adi,
                Tel = x.Tel,
                Fax = x.Fax,
                Eposta_adresi = x.Eposta_adresi
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/firmalarekle")]

        public SonucModel FirmaEkle(FirmalarModel model)
        {
            if (db.Firmalar.Count(s => s.Firma_adi == model.Firma_adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen firma adı kayıtlıdır.";
                return sonuc;
            }

            Firmalar yeni = new Firmalar();

            yeni.Firma_id = model.Firma_id;
            yeni.Firma_adi = model.Firma_adi;
            yeni.Tel = model.Tel;
            yeni.Fax = model.Fax;
            yeni.Eposta_adresi = model.Eposta_adresi;
            db.Firmalar.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Firma eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/firmalarduzenle")]

        public SonucModel FirmalarDuzenle(FirmalarModel model)
        {
            Firmalar kayit = db.Firmalar.Where(s => s.Firma_id == model.Firma_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }
            kayit.Firma_id = model.Firma_id;
            kayit.Firma_adi = model.Firma_adi;
            kayit.Tel = model.Tel;
            kayit.Fax = model.Fax;
            kayit.Eposta_adresi = model.Eposta_adresi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Firma düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/firmalarsil/{firmaid}")]

        public SonucModel FirmaSil(int firmaid)
        {
            Firmalar kayit = db.Firmalar.Where(s => s.Firma_id == firmaid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            if (db.Urunler.Count(s => s.Firma_id == firmaid) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürünler kayıtlı kategori silinemez!";
                return sonuc;
            }

            db.Firmalar.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Firma silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/urunsayisi")]

        public int UrunSayisi()
        {
            return db.Urunler.Count();
        }

        #endregion

        #region Kategoriler

        [HttpGet]
        [Route("api/kategorilerliste")]

        public List<KategorilerModel> KategorilerListe()
        {
            List<KategorilerModel> liste = db.Kategoriler.Select(x => new KategorilerModel()
            {
                Kategori_id = x.Kategori_id,
                Kategori_Adi = x.Kategori_Adi

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/kategorilerbyid/{kategoriid}")]

        public KategorilerModel Kategorilerbyid(int kategoriid)
        {
            KategorilerModel kayit = db.Kategoriler.Where(s => s.Kategori_id == kategoriid).Select(x =>
            new KategorilerModel()
            {
                Kategori_id = x.Kategori_id,
                Kategori_Adi = x.Kategori_Adi
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kategorilerekle")]

        public SonucModel KategoriEkle(KategorilerModel model)
        {

            if (db.Kategoriler.Count(s => s.Kategori_Adi == model.Kategori_Adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı kayıtlıdır.";
                return sonuc;
            }

            Kategoriler yeni = new Kategoriler();
            yeni.Kategori_id = model.Kategori_id;
            yeni.Kategori_Adi = model.Kategori_Adi;
            db.Kategoriler.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/kategorilerduzenle")]

        public SonucModel KategoriDuzenle(KategorilerModel model)
        {
            Kategoriler kayit = db.Kategoriler.Where(s => s.Kategori_id == model.Kategori_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Kategori_id = model.Kategori_id;
            kayit.Kategori_Adi = model.Kategori_Adi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategoriler düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/kategorilersil/{kategoriid}")]

        public SonucModel KategoriSil(int kategoriid)
        {
            Kategoriler kayit = db.Kategoriler.Where(s => s.Kategori_id == kategoriid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            if (db.Urunler.Count(s => s.Kategori_id == kategoriid) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürünler kayıtlı kategori silinemez!";
                return sonuc;
            }

            db.Kategoriler.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Kategori silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/kategorisayisi")]

        public int KategoriSayisi()
        {
            return db.Kategoriler.Count();
        }

        #endregion

        #region  KitapKiralama

        [HttpGet]
        [Route("api/kitapkiralamaliste")]

        public List<KitapKiralamaModel> KitapKiralamaListe()
        {
            List<KitapKiralamaModel> liste = db.KitapKiralama.Select(x => new KitapKiralamaModel()
            {
                Calisan_id = x.Calisan_id,
                Musteri_id = x.Musteri_id,
                Urun_id = x.Urun_id,
                Kiralama_tarihi = x.Kiralama_tarihi,
                TeslimAlma_tarihi = x.TeslimAlma_tarihi

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/kitapkiralamabyid/{musteriid}")]

        public KitapKiralamaModel KitapKiralamabyid(int musteriid)
        {
            KitapKiralamaModel kayit = db.KitapKiralama.Where(s => s.Musteri_id == musteriid).Select(x =>
            new KitapKiralamaModel()
            {
                Calisan_id = x.Calisan_id,
                Musteri_id = x.Musteri_id,
                Urun_id = x.Urun_id,
                Kiralama_tarihi = x.Kiralama_tarihi,
                TeslimAlma_tarihi = x.TeslimAlma_tarihi
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kitapkiralamaekle")]

        public SonucModel MusteriEkle(KitapKiralamaModel model)
        {
            KitapKiralama yeni = new KitapKiralama();
            yeni.Calisan_id = model.Calisan_id;
            yeni.Musteri_id = model.Musteri_id;
            yeni.Urun_id = model.Urun_id;
            yeni.Kiralama_tarihi = model.Kiralama_tarihi;
            yeni.TeslimAlma_tarihi = model.TeslimAlma_tarihi;
            db.KitapKiralama.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Müşteri eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/kitapkiralamaduzenle")]

        public SonucModel KitapKiralamaDuzenle(KitapKiralamaModel model)
        {
            KitapKiralama kayit = db.KitapKiralama.Where(s => s.Musteri_id == model.Musteri_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Calisan_id = model.Calisan_id;
            kayit.Musteri_id = model.Musteri_id;
            kayit.Urun_id = model.Urun_id;
            kayit.Kiralama_tarihi = model.Kiralama_tarihi;
            kayit.TeslimAlma_tarihi = model.TeslimAlma_tarihi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/kitapkiralamasil/{musteriid}")]

        public SonucModel MusteriSil(int musteriid)
        {
            KitapKiralama kayit = db.KitapKiralama.Where(s => s.Musteri_id == musteriid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            db.KitapKiralama.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Üye silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/kitapkiralamasayisi")]

        public int KitapKiralamaSayisi()
        {
            return db.KitapKiralama.Count();
        }

        #endregion

        #region Siparis

        [HttpGet]
        [Route("api/siparisliste")]

        public List<SiparisModel> SiparisListe()
        {
            List<SiparisModel> liste = db.Siparis.Select(x => new SiparisModel()
            {
                Siparis_id = x.Siparis_id,
                Urun_id = x.Urun_id,
                Adet = x.Adet,
            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/siparisbyid/{siparisid}")]

        public SiparisModel Siparisbyid(int siparisid)
        {
            SiparisModel kayit = db.Siparis.Where(s => s.Siparis_id == siparisid).Select(x =>
            new SiparisModel()
            {
                Siparis_id = x.Siparis_id,
                Urun_id = x.Urun_id,
                Adet = x.Adet,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/siparisekle")]

        public SonucModel SiparisEkle(SiparisModel model)
        {
            Siparis yeni = new Siparis();
            yeni.Siparis_id = model.Siparis_id;
            yeni.Urun_id = model.Urun_id;
            yeni.Adet = model.Adet;
            db.Siparis.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sipariş eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/siparisduzenle")]

        public SonucModel SiparisDuzenle(SiparisModel model)
        {
            Siparis kayit = db.Siparis.Where(s => s.Siparis_id == model.Siparis_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Siparis_id = model.Siparis_id;
            kayit.Urun_id = model.Urun_id;
            kayit.Adet = model.Adet;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Siparişler düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/siparissil/{siparisid}")]

        public SonucModel SiparisSil(int siparisid)
        {
            Siparis kayit = db.Siparis.Where(s => s.Siparis_id == siparisid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            db.Siparis.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Sipariş silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/siparissayisi")]

        public int SiparisSayisi()
        {
            return db.Siparis.Count();
        }

        #endregion

        #region TeslimAlindi

        [HttpGet]
        [Route("api/teslimalindiliste")]

        public List<TeslimAlindiModel> TeslimAlindiListe()
        {
            List<TeslimAlindiModel> liste = db.TeslimAlindi.Select(x => new TeslimAlindiModel()
            {
                Musteri_id = x.Musteri_id,
                Urun_id = x.Urun_id,
                Teslim_Alindi = x.Teslim_Alindi

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/teslimalindibyid/{teslimalindiid}")]

        public TeslimAlindiModel TeslimAlindibyid(int teslimalindiid)
        {
            TeslimAlindiModel kayit = db.TeslimAlindi.Where(s => s.Musteri_id == teslimalindiid).Select(x =>
            new TeslimAlindiModel()
            {
                Musteri_id = x.Musteri_id,
                Urun_id = x.Urun_id,
                Teslim_Alindi = x.Teslim_Alindi
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/teslimalindiekle")]

        public SonucModel TeslimAlindiEkle(TeslimAlindiModel model)
        {
            TeslimAlindi yeni = new TeslimAlindi();
            yeni.Musteri_id = model.Musteri_id;
            yeni.Urun_id = model.Urun_id;
            yeni.Teslim_Alindi = model.Teslim_Alindi;

            db.TeslimAlindi.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Teslim Alındı eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/teslimalindiduzenle")]

        public SonucModel TeslimAlindiDuzenle(TeslimAlindiModel model)
        {
            TeslimAlindi kayit = db.TeslimAlindi.Where(s => s.Musteri_id == model.Musteri_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Musteri_id = model.Musteri_id;
            kayit.Urun_id = model.Urun_id;
            kayit.Urun_id = model.Urun_id;
            kayit.Teslim_Alindi = model.Teslim_Alindi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Teslim Alındı alanı düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/teslimalindisil/{teslimalindiid}")]

        public SonucModel TeslimAlindiSil(int teslimalindiid)
        {
            TeslimAlindi kayit = db.TeslimAlindi.Where(s => s.Musteri_id == teslimalindiid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            db.TeslimAlindi.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Teslim Alındı alanı silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/teslimalindisayisi")]

        public int TeslimAlindiSayisi()
        {
            return db.TeslimAlindi.Count();
        }

        #endregion

        #region Urunler

        [HttpGet]
        [Route("api/urunlerliste")]

        public List<UrunlerModel> UrunlerListe()
        {
            List<UrunlerModel> liste = db.Urunler.Select(x => new UrunlerModel()
            {
                Urun_id = x.Urun_id,
                Urun_adi = x.Urun_adi,
                Kategori_id = x.Kategori_id,
                Firma_id = x.Firma_id,
                Alis_fiyat = x.Alis_fiyat

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/urunlerbyid/{urunlerid}")]

        public UrunlerModel Urunlerbyid(int urunid)
        {
            UrunlerModel kayit = db.Urunler.Where(s => s.Urun_id == urunid).Select(x =>
            new UrunlerModel()
            {
                Urun_id = x.Urun_id,
                Urun_adi = x.Urun_adi,
                Kategori_id = x.Kategori_id,
                Firma_id = x.Firma_id,
                Alis_fiyat = x.Alis_fiyat

            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/urunlerekle")]

        public SonucModel UrunlerEkle(UrunlerModel model)
        {
            Urunler yeni = new Urunler();
            yeni.Urun_id = model.Urun_id;
            yeni.Urun_adi = model.Urun_adi;
            yeni.Kategori_id = model.Kategori_id;
            yeni.Firma_id = model.Firma_id;
            yeni.Alis_fiyat = model.Alis_fiyat;
            db.Urunler.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/urunlerduzenle")]

        public SonucModel UrunlerDuzenle(UrunlerModel model)
        {
            Urunler kayit = db.Urunler.Where(s => s.Urun_id == model.Urun_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Urun_id = model.Urun_id;
            kayit.Urun_adi = model.Urun_adi;
            kayit.Kategori_id = model.Kategori_id;
            kayit.Firma_id = model.Firma_id;
            kayit.Alis_fiyat = model.Alis_fiyat;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürünler düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/urunlersil/{urunid}")]

        public SonucModel UrunlerSil(int urunid)
        {
            Urunler kayit = db.Urunler.Where(s => s.Urun_id == urunid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            if (db.KitapKiralama.Count(s => s.Urun_id == urunid) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Kitap Kiralama ve Sipariş kayıtlı kategori silinemez!";
                return sonuc;
            }

            db.Urunler.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Ürün silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/Urunsayisi")]

        public int UrunlerSayisi()
        {
            return db.Urunler.Count();
        }

        #endregion

        #region Uyeler

        [HttpGet]
        [Route("api/uyelerliste")]

        public List<UyelerModel> UyelerListe()
        {
            List<UyelerModel> liste = db.Uyeler.Select(x => new UyelerModel()
            {
                Musteri_id = x.Musteri_id,
                Ad = x.Ad,
                Soyad = x.Soyad,
                Tel_no = x.Tel_no,
                Eposta_adresi = x.Eposta_adresi,
                Adres = x.Adres

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/uyelerbyid/{uyeid}")]

        public UyelerModel Uyelerbyid(int uyeid)
        {
            UyelerModel kayit = db.Uyeler.Where(s => s.Musteri_id == uyeid).Select(x =>
            new UyelerModel()
            {
                Musteri_id = x.Musteri_id,
                Ad = x.Ad,
                Soyad = x.Soyad,
                Tel_no = x.Tel_no,
                Eposta_adresi = x.Eposta_adresi,
                Adres = x.Adres
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/uyelerekle")]

        public SonucModel UyelerEkle(UyelerModel model)
        {
            Uyeler yeni = new Uyeler();
            yeni.Musteri_id = model.Musteri_id;
            yeni.Ad = model.Ad;
            yeni.Soyad = model.Soyad;
            yeni.Tel_no = model.Tel_no;
            yeni.Eposta_adresi = model.Eposta_adresi;
            yeni.Adres = model.Adres;
            db.Uyeler.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye eklendi.";
            return sonuc;
        }


        [HttpPut]
        [Route("api/uyelerduzenle")]

        public SonucModel UyelerDuzenle(UyelerModel model)
        {
            Uyeler kayit = db.Uyeler.Where(s => s.Musteri_id == model.Musteri_id).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            kayit.Musteri_id = model.Musteri_id;
            kayit.Ad = model.Ad;
            kayit.Soyad = model.Soyad;
            kayit.Tel_no = model.Tel_no;
            kayit.Eposta_adresi = model.Eposta_adresi;
            kayit.Adres = model.Adres;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üyeler düzenlendi.";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/uyelersil/{uyeid}")]

        public SonucModel UyelerSil(int uyeid)
        {
            Uyeler kayit = db.Uyeler.Where(s => s.Musteri_id == uyeid).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı.";
                return sonuc;
            }

            db.Uyeler.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = false;
            sonuc.mesaj = "Üye silindi.";

            return sonuc;
        }

        [HttpGet]
        [Route("api/uyelersayisi")]

        public int UyelerSayisi()
        {
            return db.Uyeler.Count();
        }

        #endregion

    }
}
