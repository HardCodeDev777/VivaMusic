[![Release](https://github.com/HardCodeDev777/VivaMusic/actions/workflows/release.yml/badge.svg)](https://github.com/HardCodeDev777/VivaMusic/actions/workflows/release.yml)

# VivaMusic

App for downloading music with embedded metadata. Requires **only song and artist names, no urls**.

> [!IMPORTANT]
> This app uses Youtube Music api, so may require VPN if you're from sanctioned country

## Main Features
- Requires only song and artist names
- Embeds all song metadata like albumn name, song cover, etc.
- Supports 6 audio formats: **AAC, FLAC, MP3, M4A, OPUS, VORBIS**
- Allows saving song cover as separate `.jpg` file 


&nbsp;

<div align="center">
  <table>
    <tr>
      <td align="center">
        <h4>VivaMusic interface</h4>
        <img src="https://github.com/user-attachments/assets/375d4aba-f257-4516-b0c4-042791044c60" alt="AppLook" width="350" />
      </td>
      <td align="center">
        <h4>Downloaded song example</h4>
        <img src="https://github.com/user-attachments/assets/d4d981e9-1f21-44a2-9a38-1c0a1586a895" alt="DemoPlayer" width="350" />
      </td>
    </tr>
  </table>
</div>

## Installation 

### Download binaries

Download latest verison(setup) from **GitHub Releases**.


### Build manually

> [!NOTE]
> Requires **.NET 10 SDK** and **Inno Setup(added to PATH)**

```cmd
cd src

init.bat
release.bat
```

Setup will be located in  `Output\VivaMusic_setup_.exe` and app in `\bin\Release\net10.0\win-x64\publish`


## Development

Before doing anything you need to run `init.bat`. It'll unpack `Unpack\ffmpeg.zip` to `Extenral\ffmpeg.exe`. 
> It was made for bypassing GitHub 100 mb file limit.

## License

VivaMusic is licensed under the MIT License. Other license stuff - [here](src/External/ThirdPartyNotices.txt)
