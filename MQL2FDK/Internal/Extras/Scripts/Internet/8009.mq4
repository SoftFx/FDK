//+------------------------------------------------------------------+
//|                                                Intraday_bкta.mq4 |
//|                          Copyright © 2011, Titanium Technologies |
//|                                         http://www.pasdesite.com |
//+------------------------------------------------------------------+
#property copyright "Copyright © 2011, Titanium Technologies"
#property link      "http://www.pasdesite.com"
#include <stdlib.mqh>      //librairie pour la gestion des erreurs entre autre
#include <WinUser32.mqh>   //librairie pour se servir des boоtes de dialogues

//+------------------------------------------------------------------+
//|   variables externes de l'expert advisor                         |
//+------------------------------------------------------------------+
extern int prsi = 9;       // pйriode de calcul du RSI
extern int trailing = 20;  // traling stop virtuel en pips
extern double lot = 0.1;   // taille du lot

//+------------------------------------------------------------------+
//|   variables globales de l'expert advisor                         |
//+------------------------------------------------------------------+
//----crйation des variables et de leur valeur par dйfaut
bool New_Bar=false;        // contient false s'il n'y a pas de nouveau chadelier
double tableau[10][10];    // crйation du tableau de donnйes
//int son;                   //variable contenant le contexte du son а jouer
bool busy;                 // dйtermine si l'ea est occupй ou non а chaque nouveau tick
string mess;               // contient le message de dйbogage
//double sty = 0;            // 0 => pas pris de position, 1 ou 2 sinon
string sym;                // contient le symbole du chart
int cxt = 0;               // contexte de l'EA
int volt = 0;              // relatif а la volatilitй
int compt = 0;             // 1 si volatile () a йtй exйcutй, 0 sinon
double moy;                // moyenne pour la volatilitй
int magic1 = 12345678;     // numйro magic du premier ordre
int magic2 = 87654321;     // numйro magic du deuxiиme ordre
bool achat = true;         // on peut acheter si achat est true
bool vente = true;         // on peut vendre si vente est true
int nbpos = 0;             // nombre de positions prises (2 maximum)

int error;                 // contexte de l'erreur
int ordre = 0;             // type de l'ordre, 1 achetй, 2 vendu
int clo = 0;               // stipule si l'ordre doit кtre fermй (= 1) ou non (= 0)
double amin;               // mйmorise la valeur а laquelle doit кtre fermй l'ordre d'achat
double vmax;               // mйmorise la valeur а laquelle doit кtre fermй l'ordre de vente
double anull;              // prix auquel l'ordre d'achat est а l'йquilibre
double vnull;              // prix auquel l'ordre de vente est а l'йquilibre
bool up = false;           // indique si l'ordre est gagnant de "trailing" pips en achat
bool down = false;         // indique si l'ordre est gagnant de "trailing" pips en vente


//+------------------------------------------------------------------+
//| expert initialization function                                   |
//+------------------------------------------------------------------+
int init()
  {
//----
   sym = Symbol();         // affecte le symbole du chart au string "sym"
   busy = false;           // EA pas encore occupй
   mess = "OK";            // jusqu'ici, tout va bien
   moy = (Open[1] + Close[1])/2;
   
        
//----
   return(0);
  }
//+------------------------------------------------------------------+
//| expert deinitialization function                                 |
//+------------------------------------------------------------------+
int deinit()
  {
//----
   Comment("");
   
//----
   return(0);
  }
//+------------------------------------------------------------------+
//| expert start function                                            |
//+------------------------------------------------------------------+


int start()
  {
//----
   mess = StringConcatenate("De la boucle start() ",mess);
   info (mess);         // print le contexte de l'EA   
   if (busy == true) {
      map();
      return;
   }
   busy = true;         // EA occupй
   map ();
   
  //busy = false;
   mess = "L\'EA n\'a plus rien а faire...";
      
//----
   return(0);
  }

//+------------------------------------------------------------------+
//| fonction de redirection vers les fonctions adйquates             |
//+------------------------------------------------------------------+
int map  () {
   //mess = "dans la fonction map ()";
   //info (mess);
   if (cxt == 0)  {
      cxt = 10;
   }
   if (cxt == 10) {
      verif_ordres ();
   }
   if (cxt == 20) {
      calcul ();
   }
   if (cxt == 25)  {
      mess = "attends un nouveau chandelier";
      wait_candle ();
   }
   if (cxt == 26) {
      volatile (sym, 30, moy);
      if (compt == 1)   {
         Alert ("Volatilitй vйrifiйe et volt = ", volt," avec valeur = ", moy," Euros");
      }
      info (mess);
   } 
   if (cxt == 30) {
      take_pos ();
   }
   if (cxt == 40) {
      calcul ();
   }
   if (cxt == 50) {
      keep_pos ();
      
   }
   //if (cxt == 65) {
   //   act_ordres ();
   //}

}


//+------------------------------------------------------------------+
//| calcul des valeurs du tableau                                    |
//+------------------------------------------------------------------+
void  calcul ()   {
   //mess = "dans la fonction calcul()";
   //info (mess);
   for (int j=0; j<=9; j++) {     // йnumиre les colonnes
      tableau[0][j] = iMA(sym,0,10,0,MODE_SMMA,PRICE_MEDIAN,j);   // MM10
      tableau[1][j] = iMA(sym,0,20,0,MODE_SMMA,PRICE_MEDIAN,j);   // MM20
      tableau[2][j] = tableau[0][j] - tableau[1][j];              // diffйrence MMs
      tableau[3][j] = tableau[0][j] - tableau[0][j+1];            // pente MM10
      tableau[4][j] = tableau[1][j] - tableau[1][j+1];            // pente MM20
      tableau[5][j] = Close[j] - Open[j];                         // signe chandelier
      tableau[6][j] = iRSI(sym,0,prsi,PRICE_MEDIAN,1);            // RSI
      tableau[7][j] = iStochastic(sym,0,5,3,3,MODE_SMA,0,MODE_MAIN,0);   // position achetйe, vendue ou rien
      tableau[8][j] = iLow(sym,0,j);                              // valeur min du chandelier
      tableau[9][j] = iHigh(sym,0,j);                             // valeur max du chandelier
      }
      cxt = 30;      // vers prendre ou pas position
      if (nbpos >= 1)   {
         cxt = 50;   // vers garder position ou pas
      }
   }
   
//+------------------------------------------------------------------+
//| fonction affichant le contexte de l'EA                           |
//+------------------------------------------------------------------+
void  info (string text)  {
   if (busy == true) {
      Print ("L\'EA est occupй et voici le contexte: ", text," et cxt = ", cxt," et nbpos = ", nbpos);
      }  else  {
      Print ("L\'EA n\'est pas occupй mais voici le contexte: ", text);
      }
    }

//+------------------------------------------------------------------+
//| fonction vйrifiant si des ordres ont йtй passй                   |
//+------------------------------------------------------------------+
void verif_ordres () {
   cxt = 10;
   //mess = "dans la fonction verif_ordres ()";
   //info (mess);
   for (int i = OrdersTotal() - 1; i>=0; i--)   {
      if (OrderSelect(i, SELECT_BY_POS, MODE_TRADES)) {
         if (OrderMagicNumber() == magic1)   {
            if (OrderType() == OP_BUY) {
               achat = false;
            }  else if (OrderType() == OP_SELL) {
               vente = false;
            }
         }
         if (OrderMagicNumber() == magic2)   {
            if (OrderType() == OP_BUY) {
               achat = false;
            }  else if (OrderType() == OP_SELL) {
               vente = false;
            }
          }
      }
      if (nbpos == 2)   {
         cxt = 40;
         return;
      }
   }   
   cxt = 20;  
}

//+------------------------------------------------------------------+
//| fonction dйcidant de prendre ou non une position                 |
//+------------------------------------------------------------------+
void take_pos ()  {
   //cxt = 30;
   //mess = "dans la fonction take_pos ()";
   //info (mess);
   if (compt == 0)   {
      cxt = 26;      // vйrfication volatibilitй
      return;
   }
   if (volt >= 20)   {
      mess = "Marchй trop volatile!";
      info (mess);
      cxt = 25;      // attendre nouveau chandelier
      return;
   }
   int v = 0;
   int a = 0;
   int magic, retour, num;
   if (tableau[3][1] < 0 && tableau[3][3] > 0)  {     // si pente MM10 croоt et dйcroоt
      if (tableau[6][1] >= 30)   {                    // et si RSI >= а 30
         if (tableau[5][1] < 0)   {                   // et si signe chandelier nйgatif
            v = 1;                                    // => vendre
         }
      }
   }
   if (tableau[3][1] > 0 && tableau[3][3] < 0)  {     // si pente MM10 dйcroоt et croоt
      if (tableau[6][1] <= 70)   {                    // et si RSI <= а 70
         if (tableau[5][1] > 0)   {                   // et si signe chandelier positif
            a = 1;                                    // => acheter
         }
      }
   }
   if (v != 1) {
      mess ="ne peut pas vendre techniquement";
      info (mess);
   }
   if (a != 1) {
      mess = "ne peut pas acheter techniquement";
      info (mess);
   }
   if (nbpos <= 1)   {                                // si moins de deux positions prises
      if (nbpos == 0)   {                             // si premier ordre
         magic = magic1;                              // attribution du numйro magic au premier ordre
      }
      if (nbpos == 1)   {                             // si deuxiиme ordre
         magic = magic2;                              // attirbution du numйro magic au deuxiиme ordre
      }
      if (v == 1 && vente == true)  {                  // passer ordre de vente
         retour = OrderSend(sym, OP_SELL, lot, Bid, 3, 0, 0, "Je vends!", magic, 0, Red); // prise d'ordre
         if (retour < 1) {
            error = GetLastError();
            Print ("erreur d\'ordre de vente: (",error,"): ", ErrorDescription(error));
            return (0);
         }
         OrderSelect(OrdersTotal() - 1, SELECT_BY_POS, MODE_TRADES);
         num = OrderTicket ();
         vmax = NormalizeDouble (Ask + trailing * Point, Digits);
         vnull = Bid;               // prix auquel l'ordre est а l'йquilibre
         vente = false;             // ne peut plus prendre d'ordre de vente
         PlaySound ("vente.wav");
         Comment ("pris une position de vente avec le numйro: (",num,") et vmax = ", vmax, " sur ", sym);
         mess = StringConcatenate ("pris une position de vente avec le numйro: ", num," et vmax = ", vmax);
         info (mess);            
         nbpos++;                           // incrйmentation de nbpos
         cxt = 40;                                    // vers calcul()
         return;
      }
      if (v == 1 && vente == false) {
         mess = "ne peut pas vendre car dйjа un ordre de vente!";
         info (mess);
      }
      if (a == 1 && achat == true)  {                  // passer ordre d'achat
         retour = OrderSend(sym, OP_BUY, lot, Ask, 3, 0, 0, "J\'achиte!", magic, 0, Blue); // prise d'ordre
         if (retour < 1) {
            error = GetLastError();
            Print ("erreur d\'ordre d\'achat: (",error,"): ", ErrorDescription(error));
            return (0);
         }
         OrderSelect(OrdersTotal() - 1, SELECT_BY_POS, MODE_TRADES);
         num = OrderTicket ();
         amin = NormalizeDouble (Bid - trailing * Point, Digits);
         anull = Ask;               // prix auquel l'ordre est а l'йquilibre
         achat = false;             // ne peut plus prendre d'ordre d'achat
         PlaySound ("achat.wav");
         Comment ("pris une position d\'achat avec le numйro: (",num,") et amin = ", amin, " sur ", sym);
         mess = StringConcatenate ("pris une position d\'achat avec le numйro: ", num," et amin = ", amin);
         info (mess);
         nbpos++;                           // incrйmentation de nbpos
         cxt = 40;                                    // vers calcul()
         return;
      }
      if (a == 1 && achat == false) {
         mess = "ne peut pas acheter car dйjа un ordre d\'achat!";
         info (mess);
      }
   }
   if (nbpos == 1)   {        // si pas pris de position mais si dйjа une de prise
      cxt =40;                // vers calcul
      return;
   }  else if (nbpos == 0) {
      //cxt = 25;      // pas pris de position => attendre nouveau chandeleier
      cxt = 20;         // recalculer
      return;
   }
   mess = "erreur dans le programme!!!";
   Alert (mess);
}

//+------------------------------------------------------------------+
//| fonction dйcidant de garder ou non une position                  |
//+------------------------------------------------------------------+
void keep_pos ()  {
   cxt = 50;
   //mess = "dans la fonction keep_pos ()";
   //info (mess);
   for (int i = OrdersTotal () - 1; i>= 0; i--) {
      OrderSelect(i, SELECT_BY_POS, MODE_TRADES);
      if (nbpos == 1)   {                 // si une seule position a йtй prise
         if (OrderMagicNumber () == magic1)  {
            if (achat == false)  {           // si c'est un ordre d'achat
               ordre = 1;                    // ordre d'achat
               virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
               if (clo == 1)  {              // => fermer l'ordre de magic1
                  fermer (magic1);
                  cxt = 10;                  // recommencer au dйbut
                  return;
               }
               if (tableau[2][1] < 0)  {     // si diffйrences de MMs nйgatives
                  //cxt = 40;                  // ne rien faire
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
               if (tableau[2][1] <= 3)  {    // si diffйrence de MMs quasi nulles
                  virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
                  if (clo == 1)  {           // => fermer l'ordre de magic1
                     fermer (magic1);
                     cxt = 10;               // recommencer au dйbut
                     return;
                  }
                  //cxt = 40;                  // recalculer
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
            }                                // fin d'achat
            if (vente == false)  {           // si c'est un ordre de vente
               ordre = 2;                    // ordre de vente
               virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
               if (clo == 1)  {              // => fermer l'ordre de magic1
                  fermer (magic1);
                  cxt= 10;                   // recommencer au dйbut
                  return;
               }
               if (tableau[2][1] > 0)  {     // si diffйrences de MMs positives
                  //cxt = 40;                  // ne rien faire
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
               if (tableau[2][1] <= 3)  {    // si diffйrence de MMs quasi nulles
                  virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
                  if (clo == 1)  {           // => fermer l'ordre de magic1
                     fermer (magic1);
                     cxt = 10;               // recommencer au dйbut
                     return;
                  }
                  //cxt = 40;                  // recalculer
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
            }                                // fin de vente
         }                                   // fin de magic1 pour un seul ordre de passй(redondant) 
      }                                   // fin de npos = 1
      if (nbpos == 2)   {              // si deux positions de prises
         
         if (OrderMagicNumber () == magic1)  {
            if (OrderType () == OP_BUY)   {  // si magic1 est en achat
               ordre = 1;                    // ordre d'achat
               virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
               if (clo == 1)  {              // => fermer l'ordre de magic1
                  fermer (magic1);
                  cxt = 10;                  // recommencer au dйbut
                  return;
               }
               if (tableau[2][1] < 0)  {     // si diffйrences de MMs nйgatives
                  //cxt = 40;                  // ne rien faire
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
               if (tableau[2][1] <= 3)  {    // si diffйrence de MMs quasi nulles
                  virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre  
                  if (clo == 1)  {           // => fermer l'ordre de magic1
                     fermer (magic1);
                     cxt = 10;               // recommencer au dйbut
                     return;
                  }
               }
               //cxt = 40;                  // recalculer
               if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
               return;              
            }                             // fin de achat magic1
            if (OrderType () == OP_SELL)   {    // si magic1 est en vente
               ordre = 2;                    // ordre de vente
               virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
               if (clo == 1)  {              // => fermer l'ordre de magic1
                  fermer (magic1);
                  cxt = 10;                  // recommencer au dйbut
                  return;
               }
               if (tableau[2][1] > 0)  {     // si diffйrences de MMs positives
                  //cxt = 40;                  // ne rien faire
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
               if (tableau[2][1] <= 3)  {    // si diffйrence de MMs quasi nulles
                  virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
                  if (clo == 1)  {           // => fermer l'ordre de magic2
                     fermer (magic1);
                     cxt = 10;               // recommencer au dйbut
                     return;
                  }
               }
               //cxt = 40;                  // recalculer
               if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
               return;
            }                             // fin de vente magic1
         }                                // fin de magic1
         if (OrderMagicNumber () == magic2)  {
            if (OrderType () == OP_BUY)   {  // si magic2 est en achat
               ordre = 1;                    // ordre d'achat
               virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
               if (clo == 1)  {              // => fermer l'ordre de magic1
                  fermer (magic2);
                  cxt = 10;                  // recommencer au dйbut
                  return;
               }
               if (tableau[2][1] < 0)  {     // si diffйrences de MMs nйgatives
                  cxt = 40;                  // ne rien faire
                  return;
               }
               if (tableau[2][1] <= 3)  {    // si diffйrence de MMs quasi nulles
                  virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
                  if (clo == 1)  {           // => fermer l'ordre de magic2
                     fermer (magic2);
                     cxt = 10;               // recommencer au dйbut
                     return;
                  }
                  //cxt = 40;                  // recalculer
                  if (nbpos == 2)   {
                     cxt = 40;
                  }  else if (nbpos == 1) {
                     cxt = 20;
                  }
                  return;
               }
            }                                // fin de achat magic2
            if (OrderType () == OP_SELL)   {    // si magic2 est en vente
                  ordre = 2;                    // ordre de vente
                  virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
                  if (clo == 1)  {              // => fermer l'ordre de magic2
                     fermer (magic2);
                     cxt = 10;                  // recommencer au dйbut
                     return;
                  }
                  if (tableau[2][1] > 0)  {     // si diffйrences de MMs positives
                     //cxt = 40;                  // ne rien faire
                     if (nbpos == 2)   {
                        cxt = 40;
                     }  else if (nbpos == 1) {
                        cxt = 20;
                     }
                     return;
                  }
                  if (tableau[2][1] <= 3)  {    // si diffйrence de MMs quasi nulles
                     virtual (OrderOpenPrice ());  // vйrifier trailing stop virtuel de l'ordre
                     if (clo == 1)  {           // => fermer l'ordre de magic2
                        fermer (magic2);
                        cxt = 10;               // recommencer au dйbut
                        return;
                     }
                     //cxt = 40;                  // recalculer
                     if (nbpos == 2)   {
                        cxt = 40;
                     }  else if (nbpos == 1) {
                        cxt = 20;
                     }
                     return;
                  }
            }                                   // fin de vente magic2
         }                                      // fin de magic2    
      }                                         // fin de deux positions
   }                                            // fin de for pour йnumйrer les ordres 
   //if (nbpos != 2)   {
   //   cxt = 20;      // calculer pour voir si autre position possible
   //   return;
   //}
   //cxt = 40;      // garder position et recalculer
//   cxt = 60;      // clфturer position
      
}

//+------------------------------------------------------------------+
//| fonction dйtectant les nouveaux chandeliers                      |
//+------------------------------------------------------------------+
void detectbar()                                      // Funct. detecting ..
         {                                            // .. a new bar
         //mess = "dans la fonction detectbar()";       // texte pour dйbogage
                                                      // contexte EA
         static datetime New_Time=0;                  // Time of the current bar
         New_Bar=false;                               // No new bar
         if(New_Time!=Time[0])                        // Compare time
           {
            New_Time=Time[0];                         // Now time is so
            New_Bar=true;                             // A new bar detected
           }
         }
//+------------------------------------------------------------------+
//| fonction attendant un nouveau chandelier                         |
//+------------------------------------------------------------------+
void wait_candle ()  {
   //mess = "dans la fonction wait_candle ()";
   cxt = 25;
   detectbar ();
   if (New_Bar == false)   {
      //mess = "attends un nouveau chandelier";
      //info (mess);
      detectbar ();
   }  else  {
      mess = "Un nouveau chandelier est apparu!";
      //info (mess);
      for (int i=0; i<=9; i++)   {
         Print ("La valeur max de[",i,"] est de: ", tableau[9][i]);
         Print ("La valeur min de[",i,"] est de: ", tableau[8][i]);
      }
      cxt = 20;      // recalculer tableau
   }
}   

//+------------------------------------------------------------------+
//| fonction d'analyse de la volatilitй                              |
//+------------------------------------------------------------------+
int volatile (string symbole, int nbbars, double moy) {
    volt = 0;
    for (int i = 0; i < nbbars; i++)   {
      if (iHigh(symbole,PERIOD_M5,i) > moy && iLow(symbole,PERIOD_M5,i) < moy)
      volt++;
    }
    compt++;            // test de volatilitй effectuй
    int heure = TimeHour(iTime(symbole,PERIOD_M1,nbbars));
    int minute = TimeMinute(iTime(symbole,PERIOD_M1,nbbars));
    mess = "Les chandeliers son passйs "+volt+" fois par moy depuis "+heure+" : "+minute+" min";
    cxt = 30;           // vers take_pos()
    return (volt);     //  retourne le nombre de fois oщ les chandeliers ont coupй la moyenne moy
                       //  sur nbbars chandeliers
}
    

//+------------------------------------------------------------------+
//| fonction de trailing stop virtuel/cachй                          |
//+------------------------------------------------------------------+
int virtual (double open)  {
   double suiv;
   if (ordre == 1)   {        // si ordre d'achat
      //suiv = NormalizeDouble (Bid - trailing * Point, Digits);
      suiv = Bid;
      mess = StringConcatenate("up = ", up, " et anull =", anull, " et ordre = ", ordre, " et amin = ", amin);
      info (mess);      
      if (suiv <= amin) {     // si ordre perdant de "trailing" pips =>
         clo = 1;             // clфturer l'ordre d'achat
         mess = "fermeture d\'ordre d\'achat dйcidй par Virtual()";
         info (mess);
         return (clo);
      }
      if (up == false && (suiv - trailing * Point) > amin && suiv > anull)  {    // si le dйpassement de "trailing" pips n'a pas eu lieu
         amin = (suiv - trailing * Point);  // => trailing stop de "trailing" pips
         mess = StringConcatenate("Le amin vaut: ", amin, " Euros");
         info (mess); 
      } 
      if (suiv >= (anull + trailing * Point) && (suiv - 10 * Point) > amin)   {  // si dйpassement de "trailing" pips gagants =>
         amin = (suiv - 10 * Point);    // trailing stop ramenй а 10 pips
         up = true;                    // indique que le trailing stop а "trailing" pips a eu lieu
         mess = StringConcatenate("Le amin a йtй dйpassй et vaut maintenant: ", amin, " Euros");
         info (mess); 
      }
      clo = 0;                // garder position
   }  else if (ordre == 2) {  // si ordre de vente
      suiv = Ask;
      mess = StringConcatenate("down = ", down, " et vnull =", vnull, " et ordre = ", ordre, " et vmax = ", vmax);
      info (mess);      
      if (suiv >= vmax) {     // si ordre perdant de "trailing" pips =>
         clo = 1;             // clфturer l'ordre d'achat
         mess = "fermeture d\'ordre de vente dйcidй par Virtual()";
         info (mess);
         return (clo);
      }
      if (down == false && (suiv + trailing * Point) < vmax && suiv < vnull)  {  // si le dйpassement de "trailing" pips n'a pas eu lieu
         vmax = (suiv + trailing * Point);  // => trailing stop de "trailing" pips
         mess = StringConcatenate("Le vmax vaut: ", vmax, " Euros");
         info (mess);
      } 
      if (suiv <= (vnull - trailing * Point) && (suiv + 10 * Point) < vmax)   {  // si dйpassement de "trailing" pips gagants =>
         vmax = (suiv + 10 * Point);    // trailing stop ramenй а 10 pips
         down = true;                  // indique que le trailing stop а "trailing" pips a eu lieu
         mess = StringConcatenate("Le vmax a йtй dйpassй et vaut maintenant: ", vmax, " Euros");
         info (mess); 
      }
      clo = 0;   
   }                          // fin de vйrification ordre de vente  
   return(clo);   
}                             // fin de la fonction
//+------------------------------------------------------------------+
//| fonction de clфture d'ordres                                     |
//+------------------------------------------------------------------+
void fermer (int magicnumber) {
   int retour;
   for (int i = OrdersTotal () - 1; i >=0; i--)  {
      if (OrderSelect (i, SELECT_BY_POS, MODE_TRADES) == true) {
         if (OrderMagicNumber () == magicnumber)   {
            double prix ;
            int ticket = OrderTicket ();
            if (ordre == 1)   {                   // si ordre d'achat
               prix = Bid;                        // clфturer au Bid
               retour = OrderClose (ticket, lot, prix, 3, Orange);
               if (retour < 1) {
                  error = GetLastError();
                  Print ("erreur de fermeture d\'ordre d\'achat: (",error,"): ", ErrorDescription(error));
                  return (0);
               }
               achat = true;                    // ordre d'achat dйsormais autorisй de nouveau
               up = false;                      // remise а zйro du dйpassement de "trailing" pips
               mess = "fermeture d\'ordre d\'achat";
               info (mess);
               if (Bid <= OrderOpenPrice ()) {
                  PlaySound ("boo.wav");
               }  else  {
                  PlaySound ("caisse.wav");
               }
            }  else if (ordre == 2) {             // si ordre de vente
               prix = Ask;                        // clфturer а l'Ask
               retour = OrderClose (ticket, lot, prix, 3, Orange);
               if (retour < 1) {
                  error = GetLastError();
                  Print ("erreur de fermeture d\'ordre de vente: (",error,"): ", ErrorDescription(error));
                  return (0);
               }
               vente = true;                    // ordre de vente dйsormais autorisй de nouveau
               down = false;                    // remise а zйro du dйpassement de "trailing" pips
               mess = "fermeture d\'ordre de vente";
               info (mess);
               if (Ask >= OrderOpenPrice ()) {
                  PlaySound ("boo.wav");
               }  else  {
                  PlaySound ("caisse.wav");
               }
            }                 // fin de ordre de vente
         }                    // fin de numйro magic identifiй
      }                       // fin de sйlection d'ordre
   }                          // fin de for pour йnumйrer tout les ordres
   nbpos--;                   // dйcrйmente le nombre d'ordres actifs passйs par lui-mкme
   clo = 0;                   // ne plus fermer l'ordre
   cxt = 10;                  // recommencer au dйbut
}                             // fin de la fonction
//+------------------------------------------------------------------+
//| fonction de vйrification d'ordres en cours                       |
//+------------------------------------------------------------------+
//void act_ordres ()   {
  
//}

//+------------------------------------------------------------------+