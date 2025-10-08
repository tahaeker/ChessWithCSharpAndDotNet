# Chess — .NET Chess Engine + Console + WebAPI

## Kısa açıklama

Bu repository: C# ile yazılmış bir chess engine (ChessEngine.core), bir console arayüzü (chess) ve ASP.NET Core Web API (ChessWebApi) içerir. Oyun mantığı (hamle doğrulama, en-passant, castling, move history) engine tarafında tutulur; WebAPI oyun verilerini EF Core ile persist eder.

## Önkoşullar

* .NET 9 SDK
* (Demo için) SQL Server; sunum sırasında kolaylık için InMemory DB veya SQLite tercih edilebilir.
* (Opsiyonel) dotnet-ef tool: `dotnet tool install --global dotnet-ef`

## Proje yapısı

* `ChessEngine.core/` — Oyun mantığı, domain modelleri.
* `chess/` — Konsol uygulaması; local oynama, JSON export/import.
* `ChessWebApi/` — Web API, EF Core migrations, Swagger.

## Hızlı çalışma adımları (development/demo)

1. Repo'yu klonla ve restore et:

   ```bash
   git clone <repo>
   cd <repo>
   dotnet restore
   ```

2. (Opsiyonel - SQL Server) Connection string ayarla:

   * `ChessWebApi/appsettings.Development.json` içinde `"DefaultConnection"` ayarını güncelle.

3. Migration ve DB update (SQL Server kullanıyorsan):

   ```bash
   dotnet ef database update --project ChessWebApi --startup-project ChessWebApi
   ```

4. **Demo modu (en kolay)** — WebAPI'yi In-Memory DB ile çalıştır:

   * `ChessWebApi/Program.cs` içinde `AddDbContext` satırını geçici olarak değiştir:

     ```csharp
     builder.Services.AddDbContext<ChessDbContext>(opt =>
         opt.UseInMemoryDatabase("ChessDemo"));
     ```
   * Bu sayede SQL Server kurulumu gerekmez.

5. WebAPI çalıştır:

   ```bash
   dotnet run --project ChessWebApi
   ```

   Konsolda görünen `https://localhost:NNNN` adresini aç; `/swagger` üzerinden API’yi test et.

6. Console uygulamasını çalıştır (isteğe bağlı):

   ```bash
   dotnet run --project chess
   ```

## Örnek API çağrıları (swagger veya curl)

* Yeni oyun başlat:

  ```
  POST /api/chess/newgame?WhiteName=Alice&BlackName=Bob
  ```

  curl örnek:

  ```bash
  curl -X POST "https://localhost:{PORT}/api/chess/newgame?WhiteName=Alice&BlackName=Bob"
  ```

* Tahta al (satır dizisi formatında):

  ```
  GET /api/chess/board
  ```

* Hamle yap:

  ```
  POST /api/chess/move?from=e2&to=e4
  ```

* Hamle geçmişi:

  ```
  GET /api/chess/History
  ```

## Kod ve mimari notları (sunumda vurgulanacak)

* `ChessEngine.core`: oyun mantığı tek yerde. Test yazılacak en önemli yer burası.
* `ChessWebApi/Services/ChessService.cs`: API ile domain arasındaki koordinasyonu sağlıyor; DB persistence burada.
* `ChessWebApi/Mapping/MoveMapper.cs`: Domain ↔ Entity dönüşümleri.
* `chess` console app: hızlı manual demo ve JSON export/import.

## Kısa geliştirme önerileri (öncelikli)

1. Unit tests (xUnit) — özellikle `ErrorChecker` ve `MoveValidator`.
2. Service lifetime düzeltmesi: `AddScoped<IChessService, ChessService>()`.
3. `ILogger` ile hata loglama, DB işlemlerinde try/catch.
4. DTO/Response modelleri; domain objelerini doğrudan döndürme yerine map et.
5. README’yi repo root’a ekle (bu dosya).

## İlave notlar

* Migrations mevcut (ör. `ChessWebApi/Migrations/20250928_...`) — prod-like kullanım için SQL Server ayarla.
* Sunum/demo için InMemory DB kullan; böylece ortam kurulumuna zaman harcanmaz.
