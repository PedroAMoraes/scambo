import { jwtDecode, type JwtPayload } from "jwt-decode"

export const checkAuth = (): boolean => {
    var token = localStorage.getItem("token")
    if (token) {
        const decoded: JwtPayload = jwtDecode(token)
        const expireDate = new Date(0)
        expireDate.setUTCSeconds(decoded.exp!)
        const currentTime = new Date()
        const isExpired = currentTime > expireDate
        return !isExpired
    }
    return false
}