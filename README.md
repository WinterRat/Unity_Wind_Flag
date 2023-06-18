# Unity_Wind_Flag

This project demonstrates a real-time flag simulation in Unity, reacting to live weather data from the Seoul Real-time Meteorological Agency Information API. 
The flag model is designed using Blender Mesh Subdivide(20), and the simulation uses Unity Cloth Physics, Litjson plugin, and Shader Graph to create a realistic, wind-responsive flag effect.

![ezgif com-video-to-gif](https://github.com/WinterRat/Unity_Wind_Flag/assets/126951066/9947053c-e96f-4198-811f-ea6e0a615f4f)
<img width="977" alt="Flagpp" src="https://github.com/WinterRat/Unity_Wind_Flag/assets/126951066/80c7720e-c5ec-4486-ac51-2130f74200ab">


## Features
- Real-time wind speed and direction updates from Seoul Meteorological Agency Information API. (dx: 60, dy : 126 > Seoul Meteorological Administration Transformation Grid Coordinates)
- Unity Cloth Physics to simulate the flag movement realistically.
- Use of Litjson for parsing API responses.
- Shader Graph for visually realistic flag rendering.

## Prerequisites
Unity 2020.3.25 or later
Blender 2.9 or later

## Setup and Running
1. Clone the repository:

```bash
git clone https://github.com/winterrat/Unity_Seoul_Wind.git
```
- Import the project:

Open Unity Hub, click on "Add", and select the cloned project folder.

- API Key:

Obtain an API key from the Seoul Real-time Meteorological Agency Information API and place it in the respective spot within the WeatherDataScript.cs file.
```cs
...
public class WeatherDataScript : MonoBehaviour
{
    private readonly string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst";
    private readonly string key = "Your API key";

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }
...
``` 

- Run the project:

Open the MainScene and press Play to see the flag in action.

## Modules and Files
WeatherDataScript.cs: This script fetches and handles real-time wind information from the Seoul Meteorological Agency Information API.

FlagController.cs: This script controls the behavior of the flag in response to wind changes.

Shaders/flaglg.shadergraph: Shader graph file for the flag's appearance and material interaction.

## Contributing
Feel free to fork the repository and submit pull requests for any improvements or fixes you might implement. Please follow the existing coding style for any changes.

## License
This project is released under the MIT license. See the included License.md file for more information.
