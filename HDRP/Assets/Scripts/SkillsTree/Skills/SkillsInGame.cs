using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInGame : MonoBehaviour
{
    private SkillsManager manager;
    void Start()
    {
        manager=GameObject.Find("SkillsTree").GetComponent<SkillsManager>();

        manager.add("Kategoria1","category");
        manager.add("Kategoria2","category");
        manager.add("Kategoria3","category");



        
        manager.add("Kategoria1/Zwinność"                                                      ,"Twoja postać otrząsa się po wypadku",1);

        manager.add("Kategoria1/Zwinność/Bieg"                                                 ,"Twoja postać biega o 15% szybciej!",1);
        manager.add("Kategoria1/Zwinność/Bieg/Zgrabne nóżki"                                   ,"Męczysz się o 25% mniej biegając",1);
        manager.add("Kategoria1/Zwinność/Bieg/Zgrabne nóżki/Lekkoatletyk"                      ,"Mając na sobie lekkie uzbrojenie, prawie się nie męczysz",2);
        manager.add("Kategoria1/Zwinność/Bieg/Zgrabne nóżki/Ciężki oddech"                     ,"Męczysz się o 50% mniej przy ciężkim uzbrojeniu",2);

        manager.add("Kategoria1/Zwinność/Bieg/Niepowstrzymany"                                 ,"Bieg o 30% szybszy");
        manager.add("Kategoria1/Zwinność/Bieg/Niepowstrzymany/Atak w podskoku"                 ,"Specjalny atak, podczas skoku");
        manager.add("Kategoria1/Zwinność/Bieg/Niepowstrzymany/Powstrzymajcie go"               ,"Bieg o 45% szybszy");
        manager.add("Kategoria1/Zwinność/Bieg/Niepowstrzymany/Powstrzymajcie go/Sonic"         ,"Gracz staje się niebieski, i zamienia się w Sonica");

        manager.add("Kategoria1/Zwinność/Bieg/Niespodziewany atak"                             ,"Specjalny atak, w biegu");

        manager.add("Kategoria1/Zwinność/Dobra kondycja"                                       ,"Twoja kondycja zwiększa się o 15%",1);
        manager.add("Kategoria1/Zwinność/Dobra kondycja/Poświęcenie"                           ,"Postać może biegać za punkty zdrowia, jeżeli zabraknie mu kondycji",2);
        manager.add("Kategoria1/Zwinność/Dobra kondycja/Poświęcenie/Ostatek"                   ,"Złużywasz 50% mniej zdrowia przy braku kondycji",3);
        manager.add("Kategoria1/Zwinność/Dobra kondycja/Poświęcenie/Zabezpieczenie"            ,"Kiedy masz już resztkę życia, tracisz kondycję, ale nie zginiesz od wycieńczenia",1);

        manager.add("Kategoria1/Zwinność/Dobra kondycja/Świetna kondycja"                      ,"Twoja kondycja zwiększa się o 30%",1);
        manager.add("Kategoria1/Zwinność/Dobra kondycja/Świetna kondycja/Niesamowita forma"    ,"Twoja kondycja zwiększa się o 45%",1);

        manager.add("Kategoria1/Zwinność/Udźwig"                                               ,"Udźwig zwiększony o 15%",1);
        manager.add("Kategoria1/Zwinność/Udźwig/Mocne plecy"                                   ,"Udźwig zwiększony o 30%",1);
        manager.add("Kategoria1/Zwinność/Udźwig/Mocne plecy/Stalowy kręgosłup"                 ,"Udźwig zwiększony o 45%",1);
        manager.add("Kategoria1/Zwinność/Udźwig/Mocne plecy/Stalowy kręgosłup/Bez wytchnienia" ,"Udźwig zwiększony o 60%",1);
        manager.add("Kategoria1/Zwinność/Udźwig/Wytrwały"                                      ,"Po przekroczeniu udźwigu nadal możesz biegać ale o 75% wolniej",1);
        manager.add("Kategoria1/Zwinność/Udźwig/Wytrwały/Cicha woda"                           ,"Po przekroczeniu udźwigu męczysz się mniej przy bieganiu",1);
        manager.add("Kategoria1/Zwinność/Udźwig/Wytrwały/Kolekcjoner"                          ,"Po przekroczeniu udźwigu biegasz o 50% wolniej",1);




        manager.add("Kategoria2/Walka"                                                         ,"Stajesz się odważniejszy");
        manager.add("Kategoria2/Walka/Broń"                                                    ,"Wszystkie bronie szybciej levelują");
        manager.add("Kategoria2/Walka/Łuk"                                                    ,"Wszystkie łuki szybciej levelują");
        manager.add("Kategoria2/Walka/Magia"                                                   ,"Wszystkie czary szybciej levelują");

        manager.add("Kategoria2/Walka/Broń/Wstrząs"                                            ,"Specjalny atak, przeciwnik drętwieje na kilka sekund",3);


        /*BROŃ JEDNORĘCZNA*/
        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna"                                   ,"Bronie jednoręczne zadają 15% więcej obrażeń");

        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna/Mocne uderzenie"                   ,"Bronie jednoręczne zadają 30% więcej obrażeń");
        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna/Mocne uderzenie/Bój"               ,"Bronie jednoręczne zadają 45% więcej obrażeń");

        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna/Krytyczny Atak"                    ,"Szansa na krytyczny atak bronią jednoręczną zwiększony o 5%");
        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna/Krytyczny Atak/Krzywda"            ,"Szansa na krytyczny atak bronią jednoręczną zwiększony o 10%",2);
        
        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna/Umięśniona ręka"                   ,"Ataki bronią jednoręczną są szybsze");
        manager.add("Kategoria2/Walka/Broń/Broń jednoręczna/Umięśniona ręka/Bicepsy"           ,"Ataki są błyskawiczne. To nie przez masturbacje, tylko wieloletnie praktykowanie bronią",2);

        /*BROŃ DWURĘCZNA*/
        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna"                                     ,"Bronie jednoręczne zadają 15% więcej obrażeń");

        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna/Mocne uderzenie"                     ,"Bronie jednoręczne zadają 30% więcej obrażeń");
        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna/Mocne uderzenie/Bój"                 ,"Bronie jednoręczne zadają 45% więcej obrażeń");

        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna/Krytyczny Atak"                      ,"Szansa na krytyczny atak bronią jednoręczną zwiększony o 5%");
        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna/Krytyczny Atak/Krzywda"              ,"Szansa na krytyczny atak bronią jednoręczną zwiększony o 10%",2);
        
        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna/Umięśniona ręka"                     ,"Ataki bronią jednoręczną są szybsze");
        manager.add("Kategoria2/Walka/Broń/Broń dwuręczna/Umięśniona ręka/Bicepsy"             ,"Ataki są błyskawiczne. To nie przez masturbacje, tylko wieloletnie praktykowanie bronią",2);

        /*ŁUK*/  
        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka"                                     ,"Łuki zadają 15% więcej obrażeń");

        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka/Szybka strzała"                      ,"Łukie zadają 30% więcej obrażeń");
        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka/Szybka strzała/Głęboka rana"         ,"Łuki zadają 45% więcej obrażeń");

        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka/Krytyczny Atak"                      ,"Szansa na krytyczny atak strzałą zwiększony o 5%");
        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka/Krytyczny Atak/Krzywda"              ,"Szansa na krytyczny atak strzałą zwiększony o 10%",2);

        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka/Dystans"                             ,"Strzała leci na dalszy dystans");
        manager.add("Kategoria2/Walka/Łuk/Precyzyjna ręka/Dystans/Daleki dystans"              ,"Strzała może polecieć na ogromny obszar",2);
        /*MAGIA ZNISZCZENIA*/
        manager.add("Kategoria2/Walka/Magia/Zniszczenie"                                       ,"Czary zniszczenia zadają 15% więcej obrażeń");

        manager.add("Kategoria2/Walka/Magia/Zniszczenie/Rozpałka"                              ,"Czary zniszczenia zadają 30% więcej obrażeń");
        manager.add("Kategoria2/Walka/Magia/Zniszczenie/Rozpałka/Żar"                          ,"Czary zniszczenia zadają 45% więcej obrażeń");

        manager.add("Kategoria2/Walka/Magia/Zniszczenie/Krytyczny czar"                        ,"Czary mają 5% szans na zaczarowanie krytyczne");
        manager.add("Kategoria2/Walka/Magia/Zniszczenie/Krytyczny czar/Oparzenie"              ,"Czary mają 10% szans na zaczarowanie krytyczne",2);

        manager.add("Kategoria2/Walka/Magia/Zniszczenie/Dwie pieczenie"                        ,"Czary zniszczenia zadają 15% mniej many");
        manager.add("Kategoria2/Walka/Magia/Zniszczenie/Dwie pieczenie/Cztery pieczenie"       ,"Czary zniszczenia zadają 30% mniej many");

        /*MAGIA ODRODZENIA*/
        manager.add("Kategoria2/Walka/Magia/Odrodzenie"                                       ,"Czary odrodzenia zadają 15% więcej obrażeń");

        manager.add("Kategoria2/Walka/Magia/Odrodzenie/Nadzieja"                              ,"Czary odrodzenia są o 30% skuteczniejsze");
        manager.add("Kategoria2/Walka/Magia/Odrodzenie/Nadzieja/Wiara"                        ,"Czary odrodzenia są o 45% skuteczniejsze");

        manager.add("Kategoria2/Walka/Magia/Odrodzenie/Nowe życie"                            ,"Czary odrodzenia zadają 15% mniej many");
        manager.add("Kategoria2/Walka/Magia/Odrodzenie/Nowe życie/Rozdroże "                  ,"Czary odrodzenia zadają 30% mniej many");





        manager.add("Kategoria3/Krew"                                                         ,"Płynie w nas ta młoda krew");

        manager.add("Kategoria3/Krew/Zdrowie"                                                 ,"Regeneracja życia jest 2x szybsza");
        manager.add("Kategoria3/Krew/Zdrowie/Dobra krzepliwość"                               ,"Regeneracja życia jest 4x szybsza");

        manager.add("Kategoria3/Krew/Zdrowie/Życie"                                           ,"Życie zwiększone o 25pkt");
        manager.add("Kategoria3/Krew/Zdrowie/Życie/Trwałość"                                  ,"Życie zwiększone o 50pkt");
        manager.add("Kategoria3/Krew/Zdrowie/Życie/Trwałość/Bez granic"                       ,"Życie zwiększone o 100pkt",2);

        manager.add("Kategoria3/Tarcza"                                                       ,"Tarcza broni lepiej o 15%",1);
        manager.add("Kategoria3/Tarcza/Uważność"                                              ,"Tarcza broni lepiej o 30%",1);
        manager.add("Kategoria3/Tarcza/Uważność/Pewna ręka"                                   ,"Tarcza broni lepiej o 45%",1);

        manager.add("Kategoria3/Eksperyment"                                                  ,"Nowe obiekty możliwe do wytworzenia");

        manager.add("Kategoria3/Eksperyment/Młody alchemik"                                   ,"Mikstury skuteczniejsze są 10%");
        manager.add("Kategoria3/Eksperyment/Młody alchemik/Zdrowie najważniejsze"             ,"Mikstury skuteczniejsze są 20%");

        manager.add("Kategoria3/Eksperyment/Inteligencja"                                     ,"Jesteś mondryyy");
        manager.add("Kategoria3/Eksperyment/Inteligencja/Filozof"                             ,"Uczestniczyłeś przy tworzeniu świata");
        manager.add("Kategoria3/Eksperyment/Inteligencja/Filozof/Wszechwiedza"                ,"Wiesz już jak to jest być skrybą");

        manager.add("Kategoria3/Żyłka do interesów"                                           ,"Negocjujesz ceny, ale tylko o 3%");
        manager.add("Kategoria3/Żyłka do interesów/Targowanie się"                            ,"Negocjujesz ceny o 6%");
        manager.add("Kategoria3/Żyłka do interesów/Targowanie się/Grosz w garści"             ,"Negocjujesz ceny o 12%");
        manager.add("Kategoria3/Żyłka do interesów/Targowanie się/Grosz w garści/Handlowiec"  ,"Negocjujesz ceny aż o 24%");

        manager.add("Kategoria3/Żelazo"                                                       ,"Możesz wytwarzać żelazny ekwipunek");
        manager.add("Kategoria3/Żelazo/Stal"                                                  ,"Możesz wytwarzać stalowy ekwipunek");
        manager.add("Kategoria3/Żelazo/Stal/Złoto"                                            ,"Możesz wytwarzać złoty ekwipunek");
        manager.add("Kategoria3/Żelazo/Stal/Złoto/Kryształ"                                   ,"Możesz wytwarzać kryształowy ekwipunek");
    }
}
