// Include the libraries we need
#include <OneWire.h>
#include <DallasTemperature.h>

// Data wire is plugged into port 2 on the Arduino
#define ONE_WIRE_BUS 2

#define DefaultResolution 11

// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature sensors(&oneWire);

// arrays to hold device address
DeviceAddress insideThermometer[8];

long lastUpdateTime = 0; // Переменная для хранения времени последнего считывания с датчика
const int TEMP_UPDATE_TIME = 1000; // Определяем периодичность проверок

/*
 * Setup function. Here we do the basics
 */
void setup(void)
{
  // start serial port
  Serial.begin(115200);
  //Serial.println("Dallas Temperature Microf Control");
  sensors.begin();
  //sensors.setResolution(insideThermometer, DefaultResolution);
  printInformation();
}

int CD;

void printInformation(){
  Serial.println("Informations:");
  Serial.print("Found ");
  CD = sensors.getDeviceCount();
  Serial.print(sensors.getDeviceCount(), DEC);
  Serial.println(" devices.");

  for(int i = 0; i < CD; i++){
    // report parasite power requirements
    Serial.print("Parasite power is device "+(String)i+": "); 
    if (sensors.isParasitePowerMode()) Serial.println("ON");
    else Serial.println("OFF");

    if (CD-1 == i && !sensors.getAddress(insideThermometer[i], i)) Serial.println("Unable to find address for Device "+i);
  
    // show the addresses we found on the bus
    Serial.print("Device "+(String)i+" Address: ");
    printAddress(insideThermometer[i]);
    Serial.println();
   
    Serial.print("Device "+(String)i+" Resolution: ");
    Serial.print(sensors.getResolution(insideThermometer[i]), DEC); 
    //Serial.println(" bit");
  }
  Serial.println(";");
}

// function to print the temperature for a device
void printTemperature(DeviceAddress deviceAddress, int i)
{
  // method 2 - faster
  float tempC = sensors.getTempC(deviceAddress);
  Serial.print("Device "+(String)i+" Temp C: ");
  Serial.println(tempC);
  Serial.println(";");
}

void readTemperatures(){
  for(int i = 0; i < CD; i++){
    // call sensors.requestTemperatures() to issue a global temperature 
    // request to all devices on the bus
    Serial.print("Requesting temperatures...");
    sensors.requestTemperatures(); // Send the command to get temperatures
    Serial.println("DONE");
    
    // It responds almost immediately. Let's print out the data
    printTemperature(insideThermometer[i], i); // Use a simple function to print out the data
  }
}
/*
 * Main function. It will request the tempC from the sensors and display on Serial.
 */
void loop(void)
{ 
  if(Serial.available() > 0){
    String read = Serial.readStringUntil(';');
    String type = read.substring(0,2);
    String data = read.substring(2);

    if(read == "ping") Serial.println("OK;");
    else if(type == "sr") for(int i = 0; i < CD; i++) sensors.setResolution(insideThermometer[i], data.toInt());
    else if(type == "pi") printInformation();
    else if(type == "pt") readTemperatures();
  }

  if (millis() - lastUpdateTime > TEMP_UPDATE_TIME)
  {
    lastUpdateTime = millis();
    readTemperatures();
  }
}

// function to print a device address
void printAddress(DeviceAddress deviceAddress)
{
  for (uint8_t i = 0; i < 8; i++)
  {
    if (deviceAddress[i] < 16) Serial.print("0");
    Serial.print(deviceAddress[i], HEX);
  }
}
