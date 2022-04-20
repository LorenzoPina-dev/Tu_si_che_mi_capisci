# Tu_si_che_mi_capisci
<<<<<<< HEAD
codice sorgente Server remoto

Formato pacchetto udp:
{
	Tipo:"audio/video",
	KeyUtente:” e8de33ae-0964-48b2-affc-307c74c2d481”(esempio formato key),
	Dati:"dati presi dal dispositivo (vettore dello spettrogramma per audio e punti del viso per le cam)"
}


Url gestiti dall'api:
-=opzionale

1)	sito\login:Post	return key
●	Parametri:
○	username= nome della persona
○	Password= password dell’account
●	Utilizzo:
Effettuare la login per accedere alle altre pagine restituisce un cookie

2)	sito\register:Post
●	Parametri:
○	username= nome della persona
○	password= password dell’account
○	password2= password dell’account
○	mail= mail account
●	Utilizzo:
Registrare un utente

3)	sito\resetPassword:Get
●	Parametri:
○	mail=mail a cui mandare il recupero password
●	Utilizzo:
richiedere reset password

4)	sito\cambiaPassword:PUT
●	Parametri:
○	codice=codice ricevuto per mail
○	password = nuova password dell’account
○	password2= nuova password dell’account
●	Utilizzo:
Reset password

5)	sito\key\utente\cambiaInfo  PUT
●	Parametri:		//solo quelli che cambiano
○	Username-= nome della persona
○	password-= password dell’account
○	mail-= mail account
○	immagine-= file .png che identifica l’immagine profilo
●	Utilizzo:
Serve a cambiare le info della persona

6)	sito\key\utente:GET
Utilizzo:
Restituisce le info dell’utente registrato

7)	sito\key\emozioni\add: POST
●	Parameri:
○	tipo= tipo di emozione rilevata
○	dataRilevazione-= data di quando è stata trovata
○	IdDipositivo= dispositivo che ha rilevato l’emozione
●	Utilizzo:
Serve a Inserire una nuova emozione e solo le ia avranno il 
permesso di usare questa chiamata

8)	sito\key\emozioni:GET 
●	Parametri:
○	Start-= int che indica da che emozione iniziare a leggere
○	Numero-= int che indica quante emozioni prendere
○	Tipo-= tipo di emozione da prelevare
○	Data-= data da cui iniziare a leggere le emozioni
●	Utilizzo:
Serve a prelevare le emozioni salvate relative alla persona che ha 
eseguito la login

9)	sito\key\voltoTrovato\add:POST
●	Parametri:
○	immagine= immagine rilevata
○	dataRilevazione-= data in cui si è rilevato il volto
○	idDispositivo= id del dispositivo che ha rilevato quel volto
○	idVolto-= id del volto registrato (ia ha riconosciuto la persona)
●	Utilizzo:
Serve a Inserire un nuovo volto trovato e solo le ia avranno il 
permesso di usare questa chiamata
10)	sito\key\voltoTrovato:GET
●	Parametri:
○	Start-= int che indica da che volto iniziare a leggere
○	Numero-= int che indica quanti volti prendere
○	Data-= data da cui iniziare a leggere i volti
●	Utilizzo:
Serve a prelevare i volti salvate relative alla persona che ha 
eseguito la login

11)	sito\key\voltoRegistrato\add:POST
●	Parametri:
○	immagine= immagine inserita
○	Nome= Nome volto
●	Utilizzo:
Serve a Inserire un nuovo volto trovato e solo le ia avranno il 
permesso di usare questa chiamata
12)	sito\key\voltoRegistrato:GET
●	Parametri:
○	Start-= int che indica da che volto iniziare a leggere
○	numero-= int che indica quanti volti prendere
○	data-= data da cui iniziare a leggere i volti
●	Utilizzo:
Serve a prelevare i volti salvate relative alla persona che ha 
eseguito la login
13)	sito\key\voltoRegistrato\remove\id:Delete


14)	sito\key\dispositivi\add:POST
●	Parametri:
○	nome= nome del dispositivo
○	tipo= se cam o microfono
○	ip= ip del dispositivo
○	acceso-= se il dispositivo sta lavorando, dafault true
●	Utilizzo:
Serve a Inserire un nuovo dispositivo all’account loggato

15)	sito\key\dispositivi:GET
●	Parametri:
○	Start-= int che indica da che dispositivo iniziare a leggere
○	Numero-= int che indica quante dispositivo prendere
○	Tipo-= il tipo di dispositivo da legegre
●	Utilizzo:
Serve a prelevare i dispositivi  salvate relative alla persona che ha 
eseguito la login

16)	sito\key\dispositivi\remove\id:Delete
●	Utilizzo:
Serve a rimuovere un dispositivo

17)	sito\key\skill\add:POST
●	Parametri:
○	nome= nome della skill 
○	descrizione= descrizione della skill
○	azione= stringa che identifica il comando per attivare la skill
○	idEmozione= Emozione a cui legare la skill
●	Utilizzo:
Serve a Inserire una nuova skill all’account loggato
18)	sito\key\skill:GET
●	Parametri:
○	Start-= int che indica da che skill iniziare a leggere
○	Numero-= int che indica quante skill prendere
●	Utilizzo:
Serve a prelevare le skill salvate relative alla persona che ha 
eseguito la login

19)	sito\key\skill\remove\id:Delete

20)	sito\key\obiettivi\add:POST
●	Parametri:
○	Concluso-= indica se l’obiettivo è completato 
○	idObiettivo= idObiettivo da assegnare
●	Utilizzo:
Serve ad assegnare l’obiettivo all’account loggato
21)	sito\key\obiettivi:GET
●	Parametri:
○	Start-= int che indica da che obiettivo iniziare a leggere
○	Numero-= int che indica quanti obiettivi prendere 
●	Utilizzo:
Serve a prelevare gli obiettivi salvate relative alla persona che ha 
eseguito la login

22)	sito\key\obiettivi\change\id:PUT
●	Parametri:
○	Concluso= indica se l’obiettivo è completato 
●	Utilizzo:
Serve ad assegnare l’obiettivo all’account loggato


23)	sito\key\obiettivi\remove\id:Delete
=======
codice ia riconoscimento facciale
>>>>>>> origin/IaRiconoscimentoFacciale
