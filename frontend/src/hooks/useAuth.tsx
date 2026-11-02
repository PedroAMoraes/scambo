import axios, { AxiosError } from "axios"
import { useEffect, useState, type ChangeEvent } from "react"
import { checkAuth } from "../utils/isAuth"
import { useNavigate } from "react-router"

interface ILoginData {
    email: string,
    senha: string
}
export function useAuth() {
    const [loginData, setLoginData] = useState<ILoginData>({ email: "", senha: "" })
    const [token, setToken] = useState()
    const [error, setError] = useState<string | null>()
    const [isAuth, setIsAuth] = useState<boolean>(false)
    const navigate = useNavigate()
    useEffect(() => {
        setIsAuth(checkAuth())
    }, [])
    useEffect(() => {
        if (token != null) {
            localStorage.setItem("token", token)
            setIsAuth(checkAuth())
            navigate(0)
        }
    }, [token])
    const handleInput = (e: ChangeEvent<HTMLInputElement>) => {

        const { name, value } = e.target;
        setLoginData((dadosMantidos) => ({ ...dadosMantidos, [name]: value }))
    }
    const handleLogin = async () => {
        setError(null)
        try {
            const request = await axios.post(import.meta.env.VITE_API_URL + "api/auth/login", loginData)
            setToken(request.data.token)
        }
        catch (error ) {
            if(axios.isAxiosError(error))
                if(error.status == 401)
                    setError("Email ou senha incorretos.")
                else setError("Não foi possível efetuar login.")
        }
    }
    const logout = () => {
        localStorage.removeItem("token")
        setIsAuth(false)
        navigate(0)
    }
    return { handleLogin, handleInput, isAuth, logout, error }
}