import type React from "react"
import { OrbitProgress } from "react-loading-indicators"
import Spinner from "./Spinner/Spinner"

interface BotaoProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    carregando: boolean,
    children: React.ReactNode,

}
export default function Botao({ carregando, children, ...rest }: BotaoProps) {
    return <button {...rest}>

        <div>
        {carregando ? "Carregando..." :children}
        </div>
    </button>
}