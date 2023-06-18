import {Typography} from "@mui/material";

interface ILessonFormTypography {
    text: string
    isChanged?: boolean
}

export const LessonFormTypography = ({text, isChanged = false}: ILessonFormTypography) => {
    return (
        <Typography
            className='text-truncate'
            align='center'
            sx={{
                height: '40px',
                fontWeight: '400',
                fontSize: '1rem',
                lineHeight: '40px',
                border: `1px solid ${isChanged ? 'red' : '#555555'}`,
                borderRadius: '5px',
            }}
        >
            {text}
        </Typography>
    )
}