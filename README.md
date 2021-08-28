# ErrorCorrectionCodingApp
Desktopowa aplikacja oparta na WinForms i .NET Framework 4.7.2.

Aplikacja przedstawiające działanie trzech algorytmów wykorzystywanych w sieciach teleinformatycznych do korekcji błędów sygnałów cyfrowych.
Kontrola parzystości, najprostszy z wykorzystanych algorytmów, w rezultacie dane zakodowane są powiększone o liczbę bitów kontrolnych, gdzie liczba bitów równa jest liczbie bajtów danych wejściowych.
Kodowanie Hamminga, w tym przypadku bity kontrolne są umieszczane na pozycjach będących potęgami liczby 2.
Trzeci zastosowany algorytm to cykliczna kontrola nadmiarowa(CRC). Polega ona na dodaniu n liczby bitów kontrolnych do danych wejściowych. W przypadku tej aplikacji zastosowany został algorytm CRC-16, który dodaje 16 bitów kontrolnych.

![obraz](https://user-images.githubusercontent.com/25044505/131226082-d1d7448e-bfa8-489a-9c52-681c7cc267bb.png)

