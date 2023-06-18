export const buildUrlArguments = (params: object): string => {
    const urlParams = new URLSearchParams();
    for (const [key, value] of Object.entries(params)) {
        if (value !== undefined && value !== null) {
            urlParams.append(key, value.toString());
        }
    }
    return urlParams.toString();
}