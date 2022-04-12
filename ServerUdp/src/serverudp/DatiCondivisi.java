/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package serverudp;

import java.awt.image.BufferedImage;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.AbstractQueue;
import java.util.ArrayDeque;
import java.util.Queue;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.json.JSONArray;
import org.json.JSONObject;

/**
 *
 * @author user
 */
public class DatiCondivisi {
        
        private static DatiCondivisi instance = null;
        private Queue<Messaggio> MRicevuti, MDaInviare;
        private static int idFile = 1;
        public  boolean termina=false;
        private DatiCondivisi()
        {
            MRicevuti = new ArrayDeque<>();
            MDaInviare = new ArrayDeque<Messaggio>();
        }
        public static DatiCondivisi Instance()
        {
            if (instance == null)
                instance = new DatiCondivisi();
            return instance;
        }
        public Messaggio GetMessaggioRicevuti()
        {
            synchronized(this)
            {
                return MRicevuti.poll();
            }
        }
        public Messaggio GetMessaggioDaInviare()
        {
            synchronized (this)
            {
                return MDaInviare.poll();
            }
        }
        public void AddMessaggioRicevuti(Messaggio m)
        {
            synchronized (this)
            {
                MRicevuti.add(m);
            }
        }
        public void AddMessaggioDaInviare(Messaggio m)
        {
            synchronized (this)
            {
                MDaInviare.add(m);
            }
        }
        public Messaggio Elabora(Messaggio m) throws InterruptedException, IOException
        {
            JSONObject mess = new JSONObject(m.Testo);
            if (mess.has("Tipo"))
            {
                String tipo = mess.get("Tipo").toString();
                if (tipo.toUpperCase().equals( "audio".toUpperCase())||tipo.toUpperCase().equals("video".toUpperCase()))
                {
                    String tipoFile=tipo.toUpperCase().equals( "audio".toUpperCase())? "Emozione" : "Volto";
                    String file = "./" + tipoFile+"/"+tipoFile+ (idFile++)%100 + ".json";
                    while (new File(file).exists()) {file = "./" + tipoFile+"/"+tipoFile+ (idFile++)%100 + ".json";}
                    JSONArray list=mess.getJSONArray("Dati");
                    BufferedWriter sw = new BufferedWriter(new FileWriter(file));
                    JSONObject ris=new JSONObject();
                    ris.append("KeyUtente", mess.getString("KeyUtente"));
                    ris.append("Dati", mess.getJSONArray("Dati"));
                    sw.write(ris.toString());
                    sw.flush();
                    sw.close();
                    m.Testo = "{'success':true}";
                }
                else
                    m.Testo = "{'success':false,'testo':'tipo errato'}";
            }
            else
                m.Testo = "{'success':false,'testo':'manca il tipo'}";
            return m;
        }
        @Override
        public void finalize()
        {
            try {
                BufferedWriter sw = new BufferedWriter(new FileWriter("./indice.txt"));
                sw.write(idFile);
                sw.flush();
                sw.close();
            } catch (IOException ex) {
                Logger.getLogger(DatiCondivisi.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
}
