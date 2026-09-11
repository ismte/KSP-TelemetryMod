# KSP Telemetry Mod

Kerbal Space Program (KSP) için geliştirilmiş hafif ve canlı veri takibi sağlayan bir arayüz modudur.

## Özellikler
* **Canlı Hız & İrtifa:** Aktif uzay aracının anlık hızını ve yüksekliğini görüntüler.
* **Yakıt Takibi:** Depodaki sıvı yakıt (`LiquidFuel`) miktarını ve toplam kapasiteyi takip eder.
* **Gecikme Göstergesi:** Sahne kare süresine bağlı anlık gecikmeyi (ping/latency) hesaplar.
* **Özel İkon:** KSP `ApplicationLauncher` üzerinde özel görsel buton desteği sunar.

## Kurulum
1. [Releases](../../releases) kısmından veya derlenmiş `TelemetryMod.dll` ve `icon.png` dosyalarını indirin.
2. Klasörü KSP dizininizdeki `GameData/TelemetryMod/` yoluna kopyalayın.
3. Oyunu başlatın ve uçuş ekranında sağ taraftaki ikon üzerinden paneli açın.

## Geliştirme (Build)
Projeyi yerel ortamınızda derlemek için:
```bash
dotnet build```