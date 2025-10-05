const API_URL = '/api/chess';
const boardDiv = document.getElementById('board');

// Taş simgeleri (Unicode)
const pieceSymbols = {
    "K": "♔", "Q": "♕", "R": "♖", "B": "♗", "N": "♘", "P": "♙","p": "♟",
    "k": "♚", "q": "♛", "r": "♜", "b": "♝", "n": "♞", "p": "♟", "p": "♟"
};


let selectedCell = null;

// Yeni oyun başlat
async function startNewGame() {
    const white = document.getElementById('whiteName').value;
    const black = document.getElementById('blackName').value;

    const res = await fetch(`${API_URL}/newgame?WhiteName=${white}&BlackName=${black}`, { method: 'POST' });
    const data = await res.json();
    console.log(data);

    alert(data.message);
    drawBoard();
}

// Tahtayı çiz
async function drawBoard() {
    const res = await fetch(`${API_URL}/board`);
    const board = await res.json();
    console.log(board);
    
    boardDiv.innerHTML = '';

    for (let r = 0; r <= 8; r++) {            // 0 -> üst satır
        for (let c = 0; c <= 8; c++) {       // sol -> sağ,
            const cell = document.createElement('div');
            cell.className = 'cell ' + ((7 - r + c) % 2 === 0 ? 'white' : 'black');

            // Taşı doğru yerleştir
            let piece = board[7 - r][c];    // ters satır ile doğru eşleştirme
            if (piece) {
                cell.textContent = pieceSymbols[piece] || piece;
            }

            const coord = String.fromCharCode(97 + c) + (r + 1);
            cell.dataset.coord = coord;

            cell.addEventListener('click', () => onCellClick(cell));
            boardDiv.appendChild(cell);
        }
    }
}

// Hücreye tıklama
function onCellClick(cell) {
    if (!selectedCell && cell.textContent) {
        // taş seç
        selectedCell = cell;
        cell.classList.add('selected');
    } else if (selectedCell) {
        // hamle yap
        makeMove(selectedCell.dataset.coord, cell.dataset.coord);
        selectedCell.classList.remove('selected');
        selectedCell = null;
    }
}

// Hamle yap
async function makeMove(from, to) {
    const res = await fetch(`${API_URL}/move?from=${from}&to=${to}`, { method: 'POST' });
    const data = await res.json();

    if (res.ok) {
        drawBoard();
    } else {
        alert('Error: ' + data.message);
        drawBoard();
    }
}

// Sayfa açıldığında tahtayı çiz
drawBoard();
