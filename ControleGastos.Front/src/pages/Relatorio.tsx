import { useEffect, useState } from "react";
import { RelatorioData, RelatorioPessoa, RelatorioCategoria } from "../types";
import { totaisCompletos } from "../services/relatorioService";

import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid
} from "recharts";

function Relatorio() {
  const [pessoas, setPessoas] = useState<RelatorioPessoa[]>([]);
  const [categorias, setCategorias] = useState<RelatorioCategoria[]>([]);
  const [totalReceitas, setTotalReceitas] = useState<number>(0);
  const [totalDespesas, setTotalDespesas] = useState<number>(0);
  const [saldoLiquido, setSaldoLiquido] = useState<number>(0);

  useEffect(() => {
    carregarRelatorio();
  }, []);

  async function carregarRelatorio() {
    const data: RelatorioData = await totaisCompletos();
    setPessoas(data.pessoas);
    setCategorias(data.categorias);
    setTotalReceitas(data.totalReceitas);
    setTotalDespesas(data.totalDespesas);
    setSaldoLiquido(data.saldoLiquido);
  }

  const dataGraficoPizza = [
    { name: "Receitas", value: totalReceitas },
    { name: "Despesas", value: totalDespesas }
  ];

  const dataGraficoPessoa = pessoas.map(p => ({
    nome: p.pessoa,
    saldo: p.saldo
  }));

  const dataGraficoCategoria = categorias.map(c => ({
    nome: c.categoria,
    saldo: c.saldo
  }));

  return (
    <div>
      <h2>Relatório Financeiro</h2>

      {/* CARDS */}
      <div className="cards">
        <div className="card">
          <h4>Total Receitas</h4>
          <p>R$ {totalReceitas}</p>
        </div>
        <div className="card">
          <h4>Total Despesas</h4>
          <p>R$ {totalDespesas}</p>
        </div>
        <div className="card">
          <h4>Saldo</h4>
          <p style={{ color: saldoLiquido < 0 ? "#dc2626" : "#16a34a" }}>
            R$ {saldoLiquido}
          </p>
        </div>
      </div>

      {/* DASHBOARD */}
      <div style={{ display: "flex", gap: "60px", marginBottom: "40px" }}>
        {/* PIZZA */}
        <div>
          <h3>Receitas vs Despesas</h3>
          <PieChart width={350} height={250}>
            <Pie
              data={dataGraficoPizza}
              cx="50%"
              cy="50%"
              outerRadius={90}
              dataKey="value"
              label
            >
              <Cell fill="#16a34a" />
              <Cell fill="#dc2626" />
            </Pie>
            <Tooltip />
          </PieChart>
        </div>
      </div>

      {/* BARRAS */}
      <div style={{ display: "flex", gap: "40px", marginBottom: "40px" }}>
        {/* TABELA POR PESSOA */}
        <div>
          <h3>Totais por Pessoa</h3>
          <table style={{ width: "300px", borderCollapse: "collapse" }}>
            <thead>
              <tr>
                <th>Pessoa</th>
                <th>Receitas</th>
                <th>Despesas</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {pessoas.map((p, index) => (
                <tr key={index}>
                  <td>{p.pessoa}</td>
                  <td>R$ {p.receitas}</td>
                  <td>R$ {p.despesas}</td>
                  <td style={{ color: p.saldo < 0 ? "#dc2626" : "#16a34a" }}>
                    R$ {p.saldo}
                  </td>
                </tr>
              ))}

              {/* LINHA TOTAL GERAL PESSOAS */}
              <tr style={{ fontWeight: "bold", borderTop: "2px solid #000" }}>
                <td>Total Geral</td>
                <td>R$ {pessoas.reduce((sum, p) => sum + p.receitas, 0)}</td>
                <td>R$ {pessoas.reduce((sum, p) => sum + p.despesas, 0)}</td>
                <td style={{ color: pessoas.reduce((sum, p) => sum + p.saldo, 0) < 0 ? "#dc2626" : "#16a34a" }}>
                  R$ {pessoas.reduce((sum, p) => sum + p.saldo, 0)}
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        {/* TABELA POR CATEGORIA */}
        <div>
          <h3>Totais por Categoria</h3>
          <table style={{ width: "300px", borderCollapse: "collapse" }}>
            <thead>
              <tr>
                <th>Categoria</th>
                <th>Receitas</th>
                <th>Despesas</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {categorias.map((c, index) => (
                <tr key={index}>
                  <td>{c.categoria}</td>
                  <td>R$ {c.receitas}</td>
                  <td>R$ {c.despesas}</td>
                  <td style={{ color: c.saldo < 0 ? "#dc2626" : "#16a34a" }}>
                    R$ {c.saldo}
                  </td>
                </tr>
              ))}

              {/* LINHA TOTAL GERAL CATEGORIAS */}
              <tr style={{ fontWeight: "bold", borderTop: "2px solid #000" }}>
                <td>Total Geral</td>
                <td>R$ {categorias.reduce((sum, c) => sum + c.receitas, 0)}</td>
                <td>R$ {categorias.reduce((sum, c) => sum + c.despesas, 0)}</td>
                <td style={{ color: categorias.reduce((sum, c) => sum + c.saldo, 0) < 0 ? "#dc2626" : "#16a34a" }}>
                  R$ {categorias.reduce((sum, c) => sum + c.saldo, 0)}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Relatorio;