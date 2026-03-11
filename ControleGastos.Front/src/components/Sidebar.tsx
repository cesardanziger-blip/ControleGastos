import { FaChartPie, FaUsers, FaTags, FaMoneyBill } from "react-icons/fa";
import { Link } from "react-router-dom";

function Sidebar() {
  return (
    <div className="sidebar">

      <h2 className="logo">ControleGastos</h2>

      <nav>

        <Link to="/">
          <FaUsers />
          Pessoas
        </Link>

        <Link to="/categorias">
          <FaTags />
          Categorias
        </Link>

        <Link to="/transacoes">
          <FaMoneyBill />
          Transações
        </Link>

        <Link to="/relatorio">
          <FaChartPie />
          Relatório
        </Link>

      </nav>

    </div>
  );
}

export default Sidebar;