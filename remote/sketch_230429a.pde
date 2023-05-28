/***
* El siguiente programa en processing permite hacerle tracking a dos Marcadores pasivos
* que tengan colores diferentes.
*/
// importa la librería video
import processing.video.*;
import processing.serial.*;
import processing.net.*;


//Instancio la variable para capturar la camara
Capture camara;
Server servidor;
String datosPosiciones = "";//Guarda la informacion que se enviara por el puerto.

// Instancio la variable del color que se va a buscar
color marcadorA;

int xMarcadorA = 0;
int yMarcadorA = 0;

// Distancia de semejanza en color
float semejanzaEnColor = 45;
float minimoDePixelesSemejantes = 50;

void settings() {
    // creo la ventana
    size(640, 480);
}

void setup()
{
    // Iniciar servidor en el puerto 80
    servidor = new Server(this, 80);

    //En la variable video almaceno la camra
    camara = new Capture(this, width, height, 30);
    camara.start();

    // Marcadores

    marcadorA = color(145.0,252.0,171.0); // Color real del marcador A.
}

void draw()
{   

    //verifica si la camara esta disponible
    if (camara.available())
    {
        camara.read();
        image(camara, 0, 0);
        camara.loadPixels();
        
        textSize(64);
        
        
        fill(255, 0, 0, 150);
        rect (0,0,213,160);//ROJO
        fill(255, 255, 255, 220);
        text("1", 107, 80);
        
        
        fill(255, 150, 0, 150);
        rect (213,160,213,160);//NARANJA
        fill(255, 255, 255, 220);
        text("2", 320, 240);
        
        
        fill(0, 110, 255, 150);
        rect (426,320,213,160);//AZUL
        fill(255, 255, 255, 220);
        text("3", 533, 400);
        
        
        fill(110, 180, 0, 150);
        rect (0,320,213,160);//VERDE
        fill(255, 255, 255, 220);
        text("4", 160, 400);
        
        
        fill(150, 0, 250, 50);
        rect (426,0,213,160);//MORADO
        fill(255, 255, 255, 220);
        text("5", 533, 80);
        

        float promedioXMarcadorA = 0;
        float promedioYMarcadorA = 0;

        int cantidadPixelesCoincidenConMarcadorA = 0;
                       
        //empieza a recorrer cada pixel
        for ( int x = 0; x < camara.width; x++ )
        {
            for ( int y = 0; y < camara.height; y++ )
            {

                color pixelActual = camara.pixels[x + y * camara.width];

                float cantidadRojoDelPixelActual = red(pixelActual);
                float cantidadVerdeDelPixelActual = green(pixelActual);
                float cantidadAzulDelPixelActual = blue(pixelActual);

                float cantidadRojoDelMarcadorA = red(marcadorA);
                float cantidadVerdeDelMarcadorA = green(marcadorA);
                float cantidadAzulDelMarcadorA = blue(marcadorA);

                // Calculando la distancia de similitud en "color" para el marcador A.
                float similitudEnDistanciaDelColorMarcadorA = dist(cantidadRojoDelPixelActual, cantidadVerdeDelPixelActual, cantidadAzulDelPixelActual, cantidadRojoDelMarcadorA, cantidadVerdeDelMarcadorA, cantidadAzulDelMarcadorA); // We are using the dist( ) function to compare the current color with the color we are tracking.

                // Esta muy cerca del rojo
                if (similitudEnDistanciaDelColorMarcadorA < semejanzaEnColor)
                {
                    promedioXMarcadorA += x;
                    promedioYMarcadorA += y;
                    cantidadPixelesCoincidenConMarcadorA++;
                }
            }
        }

        if ( cantidadPixelesCoincidenConMarcadorA > minimoDePixelesSemejantes )
        {
            xMarcadorA = (int) promedioXMarcadorA / cantidadPixelesCoincidenConMarcadorA;
            yMarcadorA = (int) promedioYMarcadorA / cantidadPixelesCoincidenConMarcadorA;
        }



        dibujarCentroide(marcadorA, xMarcadorA, yMarcadorA);
        
        int cuadro = enQueCuadroEsta(xMarcadorA, yMarcadorA);

        if (xMarcadorA > 0 || yMarcadorA > 0 ) {
            datosPosiciones = (width - xMarcadorA) + "," + (height - yMarcadorA) + "," + cuadro + "\n";
        } else {
            datosPosiciones = "0,0,0,0\n";
        }
        //Enviar el dato por el puerto
        servidor.write(datosPosiciones);
    }
    

}

/**
* Dibujar centroide.
*/
void dibujarCentroide (color marcador, int xMarcador, int yMarcador) {
    fill(marcador);
    strokeWeight(4.0);
    stroke(0);
    ellipse(xMarcador, yMarcador, 16, 16);
}


boolean miFuncion(){
  return true;
}

/**
* Identifica si un x,y esta dentro de un recuadro definido 
* por la ventana x1,y1 y x2,y2
*/
boolean dentroDelCuadro(int xPos,int yPos,int x1, int y1) {
  if (xPos >= x1 && xPos <= x1 + 213) {
    // 282,234
    // 213,160,213,160
    if (yPos >= y1 && yPos <= y1 + 160) {
      return true;
    }
  }
  return false;
}

/**
* Nos dice en que cuadro esta devuelve un numero que representa el cuadro
*/
int enQueCuadroEsta(int x,int y){
  if (dentroDelCuadro(x,y,0,0)){
    return 1; // ROJO 
  }
  if (dentroDelCuadro(x,y,213,160)){
    return 2; // NARANJA 
  }
    if (dentroDelCuadro(x,y,426,320)){
    return 3; // AZUL 
  }
    if (dentroDelCuadro(x,y,0,320)){
    return 4; // VERDE 
  }
    if (dentroDelCuadro(x,y,426,0)){
    return 5; // MORADO 
  }
  return 0;
}

/**
* Obtiene el color exacto del pixel donde
* se dio un click.
*/
void mousePressed() {

    int loc = mouseX + mouseY * camara.width;
    color pixelLeido = camara.pixels[loc];

    float r1 = red(pixelLeido);
    float g1 = green(pixelLeido);
    float b1 = blue(pixelLeido);
    print(r1 + " "+ g1+ " "+b1+ "\n");

    //Habilitar para hacer tracking
    //marcadorRojo = pixelLeido;
}
