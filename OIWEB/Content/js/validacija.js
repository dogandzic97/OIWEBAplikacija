function proveriTip(){
	var tip=document.getElementById('Tip').value;
	if(tip==null || tip==''){
		alert('Поље са ознаком Тип је обавезно, молимо вас да одаберете неку од опција');
		return false;
	}
	else{
		
		return true
	}
}

function proveriDatum(){
	var datum=document.getElementById('Datum').value;
	if(datum==null || datum==''){
		alert("Поље датум је обавезно");
		return false;
	}
	else{
	
		return true;
	}
}

function proveriIDSBK(){
	var idsbk=document.getElementById('IDSBK').value;
	if(idsbk==null || idsbk==''){
		alert('Информација о Спецификацији Буџетских корисника је обавезна');
		return false;
	}
	else{
		
		return true;
	}
}

function proveriNaslov(){
	var naslov=document.getElementById('Naslov').value;
	if(naslov==null || naslov==''){
		alert('Молимо вас да унесете наслов');
		return false;
	}
	else{
	
		return true;
	}
}

function proveriTekst(){
	var tekst=document.getElementById('Tekst').value;
	if(tekst==null || tekst==''){
		alert('Нисте унели текст');
		return false;
	}
	else{
	
		return true;
	}
}

function validacija(){
	var tip=document.getElementById('Tip').value;
	var datum=document.getElementById('Datum').value;
	var idsbk=document.getElementById('IDSBK').value;
	var naslov=document.getElementById('Naslov').value;
	var tekst=document.getElementById('Tekst').value;

	if(tip==null || tip=='' || datum==null || datum=='' || idsbk==null || idsbk=='' || naslov==null || naslov=='' || tekst==null || tekst==''){
		alert("Нисте попунили сва  поља");
		return false;
	}else{
		return true;
	}
}

function proveriSadrzaj(){
	var sadrzaj=document.getElementById('Sadrzaj').value;
	if(sadrzaj==null || sadrzaj==''){
		alert('Попуните поље Садржај');
		return false;
	}else{
		
		return true;
	}
}

function proveriBrojIzdanja(){
	var brojIzdanja=document.getElementById('BrojIzdanja').value;
	if(brojIzdanja==null || brojIzdanja=='' || isNaN(brojIzdanja)){
		alert('Попуните поље Број издања. Унос мора бити цео број');
		return false
	}
	else{
		
		return true;
	}
}

function validacijaCasopisi(){
	var brojIzdanja=document.getElementById('BrojIzdanja').value;
	var datum=document.getElementById('Datum').value;
	var sadrzaj=document.getElementById('Sadrzaj').value;
	var tekst=document.getElementById('Tekst').value;
	var naslov=document.getElementById('Naslov').value;

	if(brojIzdanja==null || brojIzdanja=='' || datum==null || datum=='' || sadrzaj==null || sadrzaj=='' || tekst==null || tekst=='' || naslov==null || naslov==''){
		alert("Нисте попунили сва потребна поља, молимо вас да то учините");
		return false;
	}else{
		return true;
	}
}

function proveriKorisnickoIme(){

	var korisnickoIme=document.getElementById('KorisnickoIme').value;
	if(korisnickoIme==null || korisnickoIme==''){
		alert('Нисте попунили поље Корисничко име');
		return false;
	}
	else{
		return true;
	}
}

function proveriLozinku(){
	var lozinka=document.getElementById('Lozinka').value;
	if(lozinka==null || lozinka==''){
		alert('Нисте попунили поље за лозинку');
		return false;
	}
	else{
		return true;
	}
}

function validacijaLogin(){
	var lozinka=document.getElementById('Lozinka').value;
	var korisnickoIme=document.getElementById('KorisnickoIme').value;

	if(lozinka==null || lozinka=='' || korisnickoIme==null || korisnickoIme==''){
		alert("Нисте попунили тражена поља");
		return false;
	}
	else{
		
		return true;
	}


}


function proveriOpis(){
	var opis=document.getElementById('Opis').value;
	if(opis==null || opis==''){
		alert("Молимо вас попуните поље опис");
		return false;
	}
	else{
		
		return true;
	}
}

function proveriCenu(){
	var cena=document.getElementById('Cena').value;
	if(cena==null || cena=='' || isNaN(cena)){
		alert('Морате да попуните поље за цену. Мора бити број');
		return false;
	}
	else{
		
		return true;
	}
}


function proveraOstalihIzdanja(){
	var naslov=document.getElementById('Naslov').value;
	var sadrzaj=document.getElementById('Sadrzaj').value;
	var opis=document.getElementById('Opis').value;
	var cena=document.getElementById('Cena').value;

	if(naslov==null || naslov=='' || sadrzaj==null || sadrzaj=='' || opis==null || opis=='' || cena==null || cena==''){
		alert('Молимо вас да попуните сва поља');
		return false;
	}
	else{
		return true
	}
}

function proveriPretplatnickiBroj(){
	var pretplatnickiBroj=document.getElementById('PretplatnickiBroj').value;
	if(pretplatnickiBroj==null || pretplatnickiBroj==''){
		alert('Поље Претплатнички број је обавезно. Молим вас попуните га');
		return false;
	}
	else{
		
		return true;
	}
}

function proveriNazivPravnogLica(){
	var nazivPravnogLica=document.getElementById('NazivPravnogLica').value;
	if(nazivPravnogLica==null || nazivPravnogLica==''){
		alert('Поље назив правног лица је обавезно. Молимо вас попуните га');
		return false;
	}else{
		return true;
	}
}

function proveriBrojTekucegRacuna(){
	var brojTekucegRacuna=document.getElementById('BrojTekucegRacuna').value;
	if(brojTekucegRacuna==null || brojTekucegRacuna==''){
		alert('Поље број текућег рачуна је обавезно. Молимо вас да га попуните');
		return false;
	}
	else{
		return true;
	}
}

function proveriPIB(){
	var pib=document.getElementById('PIB').value;
	if(pib==null || pib==''){
		alert('Поље ПИБ је обавезно. Молимо вас попуните га');
		return false;
	}
	else{
		return true;
	}
}


function proveriMesto(){
	var mesto=document.getElementById('Mesto').value;
	if(mesto==null || mesto==''){
		alert('Поље место је обавезно. Молимо вас да га попуните');
		return false;
	}
	else{
		return true;
	}
}

function proveriPostanskiBroj(){
	var postanskiBroj=document.getElementById('PostanskiBroj').value;
	if(postanskiBroj==null || postanskiBroj==''){
		alert('Поље Поштански Број је обавезно. Молимо вас да га попуните');
		return false;
	}
	else{
		return true;
	}
}

function proveriUlicaIBroj(){
	var ulicaIBroj=document.getElementById('UlicaIBroj').value;
	if(ulicaIBroj==null || ulicaIBroj==''){
		alert("Поље Улица и Број је обавезно. Молимо вас да га попуните");
		return false;
	}
	else{
		return true;
	}
}

function proveriIme(){
	var ime=document.getElementById('Ime').value;
	if(ime==null || ime==''){
		alert("Поље име је обавезно. Молимо вас да га попуните");
		return false;
	}
	else{
		return true;
	}
}

function proveriPrezime(){
	var prezime=document.getElementById('Prezime').value;
	if(prezime==null || prezime==''){
		alert("Поље Презиме је обавезно. Молимо вас да га попуните");
		return false;
	}
	else{
		return true;
	}
}

function proveriTelefon(){
	var telefon=document.getElementById('Telefon').value;
	if(telefon==null || telefon==''){
		alert("Морате попунити поље телефон");
		return false;
	}
	else{
		return true;
	}
}

function proveriFaks(){
	var faks=document.getElementById('Faks').value;
	if(faks==null || faks==''){
		alert("Поље факс је обавезно. Молимо вас да га попуните");
		return false;
	}
	else{
		return true;
	}
}

function proveriEmail(){

	var email=document.getElementById('Email').value;
	
	var patt = /^\w+\@([a-z]+\.)+[a-z]{3}$/;
	var result = patt.test(email);
	if(result){
		
		return false;
	}
	else{
		alert("Нисте правилно унели вашу емаил адресеу. Молимо Вас да унесете валидан мејл");
		return true;
	}
}

function proveriKolicinu(){

	var kolicina=document.getElementById('Kolicina').value;
	if(kolicina==null || kolicina=='' || isNaN(kolicina)){
		alert("Поље Количина је обавезно и мора бити број");
		return false;
	}
	else{
		return true;
	}
}

function proveraNarudzbenice(){
		var pretplatnickiBroj=document.getElementById('PretplatnickiBroj').value;
		var nazivPravnogLica=document.getElementById('NazivPravnogLica').value;
		var brojTekucegRacuna=document.getElementById('BrojTekucegRacuna').value;
		var pib=document.getElementById('PIB').value;
		var mesto=document.getElementById('Mesto').value;
		var postanskiBroj=document.getElementById('PostanskiBroj').value;
		var ulicaIBroj=document.getElementById('UlicaIBroj').value;
		var ime=document.getElementById('Ime').value;
		var prezime=document.getElementById('Prezime').value;
		var telefon=document.getElementById('Telefon').value;
		var faks=document.getElementById('Faks').value;
		var email=document.getElementById('Email').value;
		var kolicina=document.getElementById('Kolicina').value;
 
		if(pretplatnickiBroj==null || pretplatnickiBroj=='' || nazivPravnogLica==null || nazivPravnogLica=='' || brojTekucegRacuna==null || brojTekucegRacuna=='' || pib==null || pib=='' || mesto==null || mesto=='' || postanskiBroj==null || postanskiBroj=='' || ulicaIBroj==null || ulicaIBroj=='' || ime==null || ime=='' || prezime==null || prezime=='' || telefon==null || telefon=='' || faks==null || faks=='' || email==null || email=='' || kolicina==null || kolicina==''){
			alert("Молимо вас да попуните сва поља");
			return false;
		}
		else{
			return true;
		}
}

function proverioBroj(){
	var broj=document.getElementById('Broj').value;
	if(broj==null || broj=='' || isNaN(broj)){
		alert("Моли вас унесите поље број. Унесите цео број.");
		return false;
	}else{
		return true;
	}
}

function proveriPropis(){
	var broj=document.getElementById('Broj').value;
	var naslov=document.getElementById('Naslov').value;
	var tekst=document.getElementById('Tekst').value;
	var datum=document.getElementById('Datum').value;
	if(broj==null || broj=='' || naslov==null || naslov=='' || tekst==null || tekst=='' || datum==null || datum==''){
		alert("Молимо вас да попуните сва поља");
		return false;
	}else{
		return true;
	}
}
//Proveri strucna usavrsavanja sve

function proveriStrucnaUsavrsavanja(){
	var naslov=document.getElementById('Naslov').value;
	var datumOdrzavanja=document.getElementById('Datum').value;
	var adresaOdrzavanja=document.getElementById('Mesto').value;
	var program=document.getElementById('Sadrzaj').value;
	if(naslov==null || naslov=='' || datumOdrzavanja==null || datumOdrzavanja=='' || program==null || program==''){
		alert("Молимо вас да попуните сва поља");
		return false;
	}else{
		return true;
	}

}





