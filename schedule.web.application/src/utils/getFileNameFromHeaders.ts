export const getFileNameFromHeaders = (headers: Headers) => {
    const header = headers.get('content-disposition') ?? ''
    const regex = /filename[^*]?=\s*(?:"([^"]+)"|([^;]+))/i
    const matches = header.match(regex)

    if (matches && matches.length > 1) {
        return matches[1] || matches[2]
    }
}