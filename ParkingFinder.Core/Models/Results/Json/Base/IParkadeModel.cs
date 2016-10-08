using Hach.Library.Models.Base;
using Newtonsoft.Json;

namespace ParkingFinder.Core.Models.Results.Json.Base
{
    /// <summary>
    /// The Parkade Model
    /// TODO Make Comment
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IParkadeModel : IModel
    {
        #region Properties  

        [JsonProperty("type")]
        string Type { get; set; }

        [JsonProperty("bundesland")]
        string Bundesland { get; set; }

        [JsonProperty("isPublished")]
        bool IsPublished { get; set; }

        [JsonProperty("parkraumAusserBetriebText")]
        string ParkraumAusserBetriebText { get; set; }

        [JsonProperty("parkraumAusserBetrieb_en")]
        string ParkraumAusserBetriebEn { get; set; }

        [JsonProperty("parkraumBahnhofName")]
        string ParkraumBahnhofName { get; set; }

        [JsonProperty("parkraumBahnhofNummer")]
        string ParkraumBahnhofNummer { get; set; }

        [JsonProperty("parkraumBemerkung")]
        string ParkraumBemerkung { get; set; }

        [JsonProperty("parkraumBemerkung_en")]
        string ParkraumBemerkungEn { get; set; }

        [JsonProperty("parkraumBetreiber")]
        string ParkraumBetreiber { get; set; }

        [JsonProperty("parkraumDisplayName")]
        string ParkraumDisplayName { get; set; }

        [JsonProperty("parkraumEntfernung")]
        string ParkraumEntfernung { get; set; }

        [JsonProperty("parkraumGeoLatitude")]
        string Latitude { get; set; }

        [JsonProperty("parkraumGeoLongitude")]
        string Longitude { get; set; }

        [JsonProperty("parkraumHausnummer")]
        string ParkraumHausnummer { get; set; }

        [JsonProperty("parkraumId")]
        string Id { get; set; }

        [JsonProperty("parkraumIsAusserBetrieb")]
        bool ParkraumIsAusserBetrieb { get; set; }

        [JsonProperty("parkraumIsDbBahnPark")]
        bool ParkraumIsDbBahnPark { get; set; }

        [JsonProperty("parkraumIsOpenData")]
        bool ParkraumIsOpenData { get; set; }

        [JsonProperty("parkraumIsParktagesproduktDbFern")]
        bool ParkraumIsParktagesproduktDbFern { get; set; }

        [JsonProperty("parkraumKennung")]
        string ParkraumKennung { get; set; }

        [JsonProperty("parkraumName")]
        string ParkraumName { get; set; }

        [JsonProperty("parkraumOeffnungszeiten")]
        string ParkraumOeffnungszeiten { get; set; }

        [JsonProperty("parkraumOeffnungszeiten_en")]
        string ParkraumOeffnungszeitenEn { get; set; }

        [JsonProperty("parkraumParkTypName")]
        string ParkraumParkTypName { get; set; }

        [JsonProperty("parkraumParkart")]
        string ParkraumParkart { get; set; }

        [JsonProperty("parkraumReservierung")]
        string ParkraumReservierung { get; set; }

        [JsonProperty("parkraumSlogan")]
        string ParkraumSlogan { get; set; }

        [JsonProperty("parkraumSlogan_en")]
        string ParkraumSloganEn { get; set; }

        [JsonProperty("parkraumStellplaetze")]
        string ParkraumStellplaetze { get; set; }

        [JsonProperty("parkraumTechnik")]
        string ParkraumTechnik { get; set; }

        [JsonProperty("parkraumTechnik_en")]
        string ParkraumTechnikEn { get; set; }

        [JsonProperty("parkraumURL")]
        string ParkraumUrl { get; set; }

        [JsonProperty("parkraumZufahrt")]
        string ParkraumZufahrt { get; set; }

        [JsonProperty("parkraumZufahrt_en")]
        string ParkraumZufahrtEn { get; set; }

        [JsonProperty("tarif1MonatAutomat")]
        string Tarif1MonatAutomat { get; set; }

        [JsonProperty("tarif1MonatDauerparken")]
        string Tarif1MonatDauerparken { get; set; }

        [JsonProperty("tarif1MonatDauerparkenFesterStellplatz")]
        string Tarif1MonatDauerparkenFesterStellplatz { get; set; }

        [JsonProperty("tarif1Std")]
        string Tarif1Std { get; set; }

        [JsonProperty("tarif1Tag")]
        string Tarif1Tag { get; set; }

        [JsonProperty("tarif1TagRabattDB")]
        string Tarif1TagRabattDb { get; set; }

        [JsonProperty("tarif1Woche")]
        string Tarif1Woche { get; set; }

        [JsonProperty("tarif1WocheRabattDB")]
        string Tarif1WocheRabattDb { get; set; }

        [JsonProperty("tarif20Min")]
        string Tarif20Min { get; set; }

        [JsonProperty("tarif30Min")]
        string Tarif30Min { get; set; }

        [JsonProperty("tarifBemerkung")]
        string TarifBemerkung { get; set; }

        [JsonProperty("tarifBemerkung_en")]
        string TarifBemerkungEn { get; set; }

        [JsonProperty("tarifFreiparkzeit")]
        string TarifFreiparkzeit { get; set; }

        [JsonProperty("tarifFreiparkzeit_en")]
        string TarifFreiparkzeitEn { get; set; }

        [JsonProperty("tarifMonatIsDauerparken")]
        bool TarifMonatIsDauerparken { get; set; }

        [JsonProperty("tarifMonatIsParkAndRide")]
        bool TarifMonatIsParkAndRide { get; set; }

        [JsonProperty("tarifMonatIsParkscheinautomat")]
        bool TarifMonatIsParkscheinautomat { get; set; }

        [JsonProperty("tarifParkdauer")]
        string TarifParkdauer { get; set; }

        [JsonProperty("tarifParkdauer_en")]
        string TarifParkdauerEn { get; set; }

        [JsonProperty("tarifRabattDBIsBahnCard")]
        bool TarifRabattDbIsBahnCard { get; set; }

        [JsonProperty("tarifRabattDBIsParkAndRail")]
        bool TarifRabattDbIsParkAndRail { get; set; }

        [JsonProperty("tarifRabattDBIsbahncomfort")]
        bool TarifRabattDbIsbahncomfort { get; set; }

        [JsonProperty("tarifSondertarif")]
        string TarifSondertarif { get; set; }

        [JsonProperty("tarifSondertarif_en")]
        string TarifSondertarifEn { get; set; }

        [JsonProperty("tarifWieRabattDB")]
        string TarifWieRabattDb { get; set; }

        [JsonProperty("tarifWieRabattDB_en")]
        string TarifWieRabattDbEn { get; set; }

        [JsonProperty("tarifWoVorverkaufDB")]
        string TarifWoVorverkaufDb { get; set; }

        [JsonProperty("tarifWoVorverkaufDB_en")]
        string TarifWoVorverkaufDbEn { get; set; }

        [JsonProperty("zahlungKundenkarten")]
        string ZahlungKundenkarten { get; set; }

        [JsonProperty("zahlungMedien")]
        string ZahlungMedien { get; set; }

        [JsonProperty("zahlungMedien_en")]
        string ZahlungMedienEn { get; set; }

        [JsonProperty("zahlungMobil")]
        string ZahlungMobil { get; set; }

        #endregion

        #region Additional Properties

        IOccupancyModel OccupancyModel { get; set; }

        #endregion
    }
}
