namespace ParkingFinder.Core.Models.Results.Json.Base.Implementations
{
    /// <summary>
    /// The Parkade Model
    /// TODO Make Comment
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class ParkadeModel : IParkadeModel
    {
        public string Type { get; set; }

        public string Bundesland { get; set; }

        public bool IsPublished { get; set; }

        public string ParkraumAusserBetriebText { get; set; }

        public string ParkraumAusserBetriebEn { get; set; }

        public string ParkraumBahnhofName { get; set; }

        public string ParkraumBahnhofNummer { get; set; }

        public string ParkraumBemerkung { get; set; }

        public string ParkraumBemerkungEn { get; set; }

        public string ParkraumBetreiber { get; set; }

        public string ParkraumDisplayName { get; set; }

        public string ParkraumEntfernung { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string ParkraumHausnummer { get; set; }

        public string Id { get; set; }

        public bool ParkraumIsAusserBetrieb { get; set; }

        public bool ParkraumIsDbBahnPark { get; set; }

        public bool ParkraumIsOpenData { get; set; }

        public bool ParkraumIsParktagesproduktDbFern { get; set; }

        public string ParkraumKennung { get; set; }

        public string ParkraumName { get; set; }

        public string ParkraumOeffnungszeiten { get; set; }

        public string ParkraumOeffnungszeitenEn { get; set; }

        public string ParkraumParkTypName { get; set; }

        public string ParkraumParkart { get; set; }

        public string ParkraumReservierung { get; set; }

        public string ParkraumSlogan { get; set; }

        public string ParkraumSloganEn { get; set; }

        public string ParkraumStellplaetze { get; set; }

        public string ParkraumTechnik { get; set; }

        public string ParkraumTechnikEn { get; set; }

        public string ParkraumUrl { get; set; }

        public string ParkraumZufahrt { get; set; }

        public string ParkraumZufahrtEn { get; set; }

        public string Tarif1MonatAutomat { get; set; }

        public string Tarif1MonatDauerparken { get; set; }

        public string Tarif1MonatDauerparkenFesterStellplatz { get; set; }

        public string Tarif1Std { get; set; }

        public string Tarif1Tag { get; set; }

        public string Tarif1TagRabattDb { get; set; }

        public string Tarif1Woche { get; set; }

        public string Tarif1WocheRabattDb { get; set; }

        public string Tarif20Min { get; set; }

        public string Tarif30Min { get; set; }

        public string TarifBemerkung { get; set; }

        public string TarifBemerkungEn { get; set; }

        public string TarifFreiparkzeit { get; set; }

        public string TarifFreiparkzeitEn { get; set; }

        public bool TarifMonatIsDauerparken { get; set; }

        public bool TarifMonatIsParkAndRide { get; set; }

        public bool TarifMonatIsParkscheinautomat { get; set; }

        public string TarifParkdauer { get; set; }

        public string TarifParkdauerEn { get; set; }

        public bool TarifRabattDbIsBahnCard { get; set; }

        public bool TarifRabattDbIsParkAndRail { get; set; }

        public bool TarifRabattDbIsbahncomfort { get; set; }

        public string TarifSondertarif { get; set; }

        public string TarifSondertarifEn { get; set; }

        public string TarifWieRabattDb { get; set; }

        public string TarifWieRabattDbEn { get; set; }

        public string TarifWoVorverkaufDb { get; set; }

        public string TarifWoVorverkaufDbEn { get; set; }

        public string ZahlungKundenkarten { get; set; }

        public string ZahlungMedien { get; set; }
     
        public string ZahlungMedienEn { get; set; }

        public string ZahlungMobil { get; set; }

        #region Additional Properties

        public IOccupancyModel OccupancyModel { get; set; }

        #endregion
    }
}
