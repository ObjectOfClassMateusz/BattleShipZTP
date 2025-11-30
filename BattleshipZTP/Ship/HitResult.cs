namespace BattleshipZTP;

public enum HitResult
{
    // Opcja 1: Trafiony ale nie zatopiony
    Hit,
    
    // Opcja 2: Trafiony i zatopiony
    Sunk,
    
    // Opcja 3: Pudło
    Miss,
    
    // Opcja 4: Już trafiony (opcjonalnie, jesli chcemy obsłużyć powtórne trafienia)
    AlreadyHit
}