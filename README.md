# myEnergyUsage

<p align="center">
<img src=myEnergyUsage/images/icon.png alt="Icon"/>
</p>

This apps main task is to allow you to visualise the energy usage that has been recorded into a comma separated value (csv) file. 

## How it works.
 
Open the app and you will have the following GUI.

<p align="center">
<img src=myEnergyUsage/images/startup.png alt="Startup"/>
</p>

Buttons and other UI Controls will appear or disappear depending on the choices you make as you work with the app. There are two tabs one for charts and one for Costs. Cost is where we enter the cost of the energy, we will see later that this is saved to a json file. The charts tab gets its data from Comms Separated Files (.csv).

## Data Usage

### CSV Data Files

The folder structure where the data is stored and how it is named is important. The app starts to look under the folder Data into subfolders for the year, then under the year into subfolders for the months (Sentence case for months). Under the months are folders for daily and halfhourly. This is illustrated below: 

<p align="center">
<img src=myEnergyUsage/images/filestructure.png alt="File Structure"/>
</p>

The CSV file name explains what meter it belongs to e.g. 
•	ChurchE.csv is the Electric meter for the church. 
•	PresbyteryG.csv is the gas meter for the presbytery.

There are two types of data that can be charted, the half hourly energy usage and the daily usage. The layout of the csv file is shown below:

##### HalfHour	
<p align="center">
<img src=myEnergyUsage/images/halfhour_layout.png alt="Half Hour Layout"/>
</p>

##### Daily
<p align="center">
<img src=myEnergyUsage/images/daily_layout.png alt="Daily Layout"/>
</p> 	 


### Select Data

Click on the “Select Data” button and choose the folder where all your data is stored. The app will now search that folder and list the data that it finds.   

<p align="center">
<img src=myEnergyUsage/images/select_data.png alt="Select Data"/>
</p> 	

Once you have selected the data you can click on “Show Data” and the chart will be drawn. Here we draw the chart for one day only.

<p align="center">
<img src=myEnergyUsage/images/show_chart.png alt="Show Chart"/>
</p> 

### Explanation of the Half Hourly charts
<p align="center">
<img src=myEnergyUsage/images/halfhour_explanation.png alt="Half Hour Explanation"/>
</p> 

The chart is shaded in such a way as to give a pictorial explanation of the different times in a day. 
•	Energy usage is shaded dark blue
•	Night time is shaded light blue
•	Day light is shaded yellow
•	Peak period for energy is shaded grey
•	Sunrise is indicated with a red vertical line
•	Sunset is indicated with an orange vertical line

An interesting fact is that overnight there is a constant use of energy. As soon as the sun comes up and there is no cloud, we see the solar panels start to produce energy and the amount of energy that needs to be imported decreases. The decrease increases as the suns angle increases as the morning progresses. 

<p align="center">
<img src=myEnergyUsage/images/solar.png alt="Solar"/>
</p> 

### Tooltips
If you put your mouse over a charted bar, you will get a tooltip telling you the date, time, energy used and cost for the half hour the bar represents.

<p align="center">
<img src=myEnergyUsage/images/tooltip.png alt="Tooltip"/>
</p>

### Viewing multiple days
You can view more than one day in the same chart. Just check the days you want to view in the selection box.

<p align="center">
<img src=myEnergyUsage/images/multichart2.png alt="Multiple Days"/>
</p>

Click show chart and you will see the various days in a chart.

<p align="center">
<img src=myEnergyUsage/images/multichart1.png alt="Multiple Days"/>
</p>

If you choose days that are not consecutive you will get gaps in the chart where the missing days will have been.

<p align="center">
<img src=myEnergyUsage/images/multichart3.png alt="Multiple Days"/>
</p>

Note the day on the far right of the chart does not have a peak time. Depending on your tariff you may find you can have a whole day on off-peak energy.

### Viewing the daily data

Check the radio button called Daily and the UI will reset.

<p align="center">
<img src=myEnergyUsage/images/daily_initial.png alt="Daily"/>
</p>

Click “Show Chart” to display the daily totals for the month

<p align="center">
<img src=myEnergyUsage/images/daily_show_chart.png alt="Show Daily Chart"/>
</p>

### Save Chart Image
Click on the “Save Chart” and you will be able to select where you save the image and what you call it. The size you save will depend on the size you have the app working at. For the best results maximise the app.

# Energy Costs

### Json file
The tariff data is stored in a .json file which is found in the same directory as the apps exe file. The layout of this file is shown below.

<p align="center">
<img src=myEnergyUsage/images/json_layout.png alt="JSON file layout"/>
</p>

### Setting the tariff
The tariff that you are on may change and you will want to calculate the energy cost with the correct tariffs. Other things that may change could be the standing day charge or the NRAB.

<i>Nuclear Regulated Asset Base (NRAB): Starting from November 1, 2025, all energy suppliers are be required to pay into the Nuclear RAB Levy to fund the development and operation of nuclear projects, including Sizewell C. The initial rate for the levy is set at £3.455 per MWh, which means a typical small business using 10 000 kWh per year would pay about £35 extra annually.</i>

<p align="center">
<img src=myEnergyUsage/images/tariff1.png alt="Setting Tariff"/>
</p>

You can 
•	Edit an entry by clicking on the tariff entry on the right-hand side.
•	Create a new entry into the .json file by clicking on the new button. 
•	Save the updated or new entry by clicking on the save button.
•	Delete the entry by clicking on the delete button  

End of File




