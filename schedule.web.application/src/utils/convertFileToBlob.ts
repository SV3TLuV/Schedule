export const convertFileToBlob = (file: File): Promise<Blob> => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = () => {
            const result = reader.result;
            if (result) {
                const blob = new Blob([result], { type: file.type });
                resolve(blob);
            } else {
                reject(new Error('Не удалось прочитать файл'));
            }
        };

        reader.readAsArrayBuffer(file);
    });
};