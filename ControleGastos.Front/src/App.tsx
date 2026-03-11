import { BrowserRouter, Routes, Route } from "react-router-dom";

import Pessoas from "./pages/Pessoas";
import Categorias from "./pages/Categorias";
import Transacoes from "./pages/Transacoes";
import Relatorio from "./pages/Relatorio";

import Sidebar from "./components/Sidebar";
import Header from "./components/Header";

function App() {
  return (
    <BrowserRouter>
      <div className="layout">
        <Sidebar /> {/* Lado esquerdo fixo */}
        <div className="main">
          <Header />
          <div className="content">
            <Routes>
              <Route path="/" element={<Pessoas />} />
              <Route path="/categorias" element={<Categorias />} />
              <Route path="/transacoes" element={<Transacoes />} />
              <Route path="/relatorio" element={<Relatorio />} />
            </Routes>
          </div>
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;